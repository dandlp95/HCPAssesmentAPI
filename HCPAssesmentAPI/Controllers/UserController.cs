using HCPAssesmentAPI.Models;
using HCPAssesmentAPI.Repositories;
using MagicVilla_VillaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace HCPAssesmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {   
        // UsersRepository will handle all API requests.
        private readonly IUsersRepository _jsonplaceholderRepo;
        // APIResponse Wraps API response metadata and results to keep the API response format consistent.
        private readonly APIResponse _response; 
        public UserController(IUsersRepository jsonplaceholderRepo)
        {
            _jsonplaceholderRepo = jsonplaceholderRepo;
            _response = new();
        }

        // Gets users from jsonplaceholder API.
        [HttpGet] 
        public async Task<ActionResult<APIResponse>> GetJsonPlaceholderUsers()
        {
            try
            {
                IEnumerable<User> users = await _jsonplaceholderRepo.GetAllUsers();
                _response.Result = users;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                _response.StatusCode = HttpStatusCode.InternalServerError;
                return _response;
            }
        }

        // Makes requests to Home Care Pulse API.
        [HttpPost] 
        public async Task<ActionResult<APIResponse>> PostUsers([FromBody] HCPRequest request)
        {
            try
            {
                var response = await _jsonplaceholderRepo.PostUsers(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string res = response.Content.ReadAsStringAsync().Result;
                    dynamic deserialized = JsonConvert.DeserializeObject<SuccessResponse>(res);
                    _response.StatusCode = response.StatusCode;
                    _response.Result = deserialized;
                    return _response;
                }
                else
                {
                    _response.StatusCode = response.StatusCode;
                    _response.IsSuccess = false;
                    _response.Result = response.Content.ReadAsStringAsync().Result;
                    return BadRequest(_response);
                }

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                _response.StatusCode = HttpStatusCode.InternalServerError;
                return _response;
            }


        }
    }
}
