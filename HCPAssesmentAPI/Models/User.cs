namespace HCPAssesmentAPI.Models
{
    /// <summary>
    /// Model class to comply with the format required by the Home Care Pulse API.
    /// Users fetched from jsonplaceholder API will be transformed into this model.
    /// </summary>
    public class User
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string company_name { get; set; }
        public string company_full_address { get; set; }
        public string website { get; set; }
        public string phone { get; set; }
    }
}