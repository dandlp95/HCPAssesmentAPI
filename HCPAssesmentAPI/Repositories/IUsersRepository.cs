using HCPAssesmentAPI.Models;

namespace HCPAssesmentAPI.Repositories
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<List<UserJson>> GetUsersJsonPlaceholder();
        Task<HttpResponseMessage> PostUsers(HCPRequest request); 
    }
}
