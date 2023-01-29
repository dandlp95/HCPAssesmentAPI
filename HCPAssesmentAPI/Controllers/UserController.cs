using HCPAssesmentAPI.Models;
using HCPAssesmentAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HCPAssesmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IJsonPlaceHolderRepository _jsonplaceholderRepo;
        public UserController(IJsonPlaceHolderRepository jsonplaceholderRepo)
        {
            _jsonplaceholderRepo = jsonplaceholderRepo;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetJsonPlaceholderUsers()
        {
            IEnumerable<User> users = await _jsonplaceholderRepo.GetAllUsers();
            return Ok(users);
        }
    }
}
