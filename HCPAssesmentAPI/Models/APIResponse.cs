using System.Net;

namespace MagicVilla_VillaAPI.Models
{
    /// <summary>
    /// Wraps API responses results and metadata.
    /// </summary>
    /// <remarks>
    /// Since this API works with other 2 other third-party API's, this class will
    /// work as a wrapper to keep API response consistent.
    /// </remarks>
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<String> ErrorMessages { get; set; }
        public object Result { get; set; }
    }
}
