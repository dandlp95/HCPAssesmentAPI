using System.ComponentModel.DataAnnotations;

namespace HCPAssesmentAPI.Models
{
    public class HCPRequest
    {
        [Required]
        public string userid { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string outputtype { get; set; }
        [Required]
        public List<User> users { get; set; }

    }
}
