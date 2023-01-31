using HCPAssesmentAPI.Models;
using Newtonsoft.Json;
using System.Text;

namespace HCPAssesmentAPI.Repositories
{
    /// <summary>
    /// Handles all API requests.
    /// </summary>
    /// <remarks>
    /// Decouples API requests from controller class.
    /// </remarks>
    public class UsersRepository : IUsersRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _HCPhttpClient;
        public UsersRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _HCPhttpClient = httpClientFactory.CreateClient();
        }

        public async Task<List<UsersJson>> GetAllUsersJsonPlaceholder()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://jsonplaceholder.typicode.com/users");
            request.Headers.Add("Accept", "application/json");

            var response = await _HCPhttpClient.SendAsync(request);

            string res = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<List<UsersJson>>(res);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            string[] titles = { "Mr.", "Mrs." };

            List<UsersJson> data = await GetAllUsersJsonPlaceholder();
            IEnumerable<User> users = data.Select(user => new User
            {
                first_name = splitFullName(user.name, titles)[0],
                last_name = splitFullName(user.name, titles)[1],
                company_name = user.company.name,
                company_full_address = user.address.street + ", " + user.address.suite + ", " + user.address.city + ", " + user.address.zipcode,
                phone = formatPhoneNumber(user.phone, new string[] { "-", ".", "(", ")" }),
                website = user.website
            });

            return users;
        }

        public async Task<HttpResponseMessage> PostUsers(HCPRequest request)
        {
            string HCPUri = "https://dev.app.homecarepulse.com/Primary/?FlowId=7423bd80-cddb-11ea-9160-326dddd3e106&Action=api";
            string requestBodyJson = JsonConvert.SerializeObject(request);
            StringContent bodyContent = new(requestBodyJson, Encoding.UTF8, "application/json");

            var response = await _HCPhttpClient.PostAsync(HCPUri, bodyContent);

            return response;
        }
        /// <summary>
        /// Formats full name to comply with Home Care Pulse API format for first_name and last_name.
        /// </summary>
        /// <remarks>
        /// This method assumes there are no middle names. All words after the first word will be assumed to be the last name.
        /// </remarks>
        /// <param name="fullName">String with the user's full name.</param>
        /// <param name="titles">string array that includes titles that should be removed from the full name.
        /// For example, Mr. or Mrs.</param>
        /// <returns>Returns string array with 2 elements. First element is the first name and second one the last name.</returns>
        public string[] splitFullName(string fullName, string[] titles)
        {
            int wordCount = fullName.Split(" ").Length;
            if (wordCount == 0)
            {
                return new string[] { " ", " " };
            }
            if (wordCount == 1)
            {
                return new string[] { fullName, " " };
            }

            foreach (string title in titles)
            {
                fullName = fullName.Replace(title + " ", string.Empty);
            }

            int firstSpaceIdx = fullName.IndexOf(" ");
            string firstName = fullName.Substring(0, firstSpaceIdx);
            string lastName = fullName.Substring(firstSpaceIdx + 1);

            return new string[] { firstName, lastName };
        }

        /// <summary>
        /// Formats user phone number to comply with Home Care Pulse API request format
        /// </summary>
        /// <remarks>
        /// Method assumes phone extension will always be at the end.
        /// Method assumes any final extra digits is the area code.
        /// </remarks>
        /// <param name="phoneNumber">single string containing phone number</param>
        /// <param name="unwantedChars">Any characters to remove from phone number.</param>
        /// <returns>single string containing properly formatted phone number</returns>

        public string formatPhoneNumber(string phoneNumber, string[] unwantedChars)
        {
            // Next block of code removes phone extension.
            if (phoneNumber.Contains("x"))
            {
                int xIndex = phoneNumber.IndexOf("x");
                string substringToRemove = phoneNumber.Substring(xIndex);
                phoneNumber = phoneNumber.Replace(substringToRemove, string.Empty);
            }

            // Loops through string and removes unwanted characters.
            foreach (string character in unwantedChars)
            {
                phoneNumber = phoneNumber.Replace(character, string.Empty);
            }

            phoneNumber = phoneNumber.Replace(" ", string.Empty);

            // Any final extra digits are assumed to be the area code.
            // It starts removing first string character until there are 10.
            while (phoneNumber.Length > 10)
            {
                phoneNumber = phoneNumber.Remove(0, 1);
            }

            return phoneNumber;
        }


    }
}

