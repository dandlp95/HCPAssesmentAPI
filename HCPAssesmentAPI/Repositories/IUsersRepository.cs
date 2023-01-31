using HCPAssesmentAPI.Models;

namespace HCPAssesmentAPI.Repositories
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<List<UsersJson>> GetAllUsersJsonPlaceholder();
        Task<HttpResponseMessage> PostUsers(HCPRequest request); 
    }
}
