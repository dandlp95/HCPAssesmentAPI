namespace HCPAssesmentAPI.Models
{
    public class UsersJson
    {
        public string name { get; set; }
        public Company company { get; set; }

        public Address address { get; set; }
        public string website { get; set; }
        public string phone { get; set; }
    }

    public class Company
    {
        public string name { get; set; }
    }

    public class Address
    {
        public string street { get; set; }
        public string suite { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
    }

}
