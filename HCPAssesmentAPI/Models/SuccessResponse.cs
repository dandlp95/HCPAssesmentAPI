namespace HCPAssesmentAPI.Models
{
    public class SuccessResponse
    {
        public DataReceived success { get; set; }
    }

    public class DataReceived
    {
        public List<User> data_received { get; set; }
    }
}


