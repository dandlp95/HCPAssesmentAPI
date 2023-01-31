namespace HCPAssesmentAPI.Models
{
    /// <summary>
    /// Model Class to deserialize succesful API response from Home Care Pulse API.
    /// </summary>
    public class SuccessResponse
    {
        public DataReceived success { get; set; }
    }

    public class DataReceived
    {
        public List<User> data_received { get; set; }
    }
}


