using HCPAssesmentAPI.Models;
using HCPAssesmentAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Xml;

namespace HCPAssesmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IJsonPlaceHolderRepository _jsonplaceholderRepo;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _HCPhttpClient;
        public UserController(IJsonPlaceHolderRepository jsonplaceholderRepo, IHttpClientFactory httpClientFactory)
        {
            _jsonplaceholderRepo = jsonplaceholderRepo;
            _httpClientFactory = httpClientFactory;
            _HCPhttpClient = _httpClientFactory.CreateClient();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetJsonPlaceholderUsers()
        {
            IEnumerable<User> users = await _jsonplaceholderRepo.GetAllUsers();
            return Ok(users);
        }
        [HttpPost]
        public async Task<ActionResult> PostUsers([FromBody] HCPRequest request)
        {

            string HCPUri = "https://dev.app.homecarepulse.com/Primary/?FlowId=7423bd80-cddb-11ea-9160-326dddd3e106&Action=api";
            string requestBodyJson = JsonConvert.SerializeObject(request);
            StringContent bodyContent = new(requestBodyJson, Encoding.UTF8, "application/json");

            var response = await _HCPhttpClient.PostAsync(HCPUri, bodyContent);
            string res = response.Content.ReadAsStringAsync().Result;
            dynamic deserialized = JsonConvert.DeserializeObject<object>(res);
            return Ok(response);
        }
    }
}
