using HCPAssesmentAPI.Models;
using Newtonsoft.Json;

namespace HCPAssesmentAPI.Repositories
{
    public class JsonPlaceHolderRepository : IJsonPlaceHolderRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public JsonPlaceHolderRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<UsersJson>> GetAllUsersJsonPlaceholder()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://jsonplaceholder.typicode.com/users");
            request.Headers.Add("Accept", "application/json");

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.SendAsync(request);

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
                phone = user.phone,
                website = user.website
            });

            return users;
        }

        

        public string[] splitFullName(string fullName, string[] titles)
        {
            int wordCount = fullName.Split(' ').Length;
            if(wordCount == 0) {
                return new string[] { " ", " " };
            }
            if(wordCount == 1)
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
    }
}

