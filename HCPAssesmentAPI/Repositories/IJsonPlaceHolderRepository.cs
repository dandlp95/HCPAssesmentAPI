using HCPAssesmentAPI.Models;

namespace HCPAssesmentAPI.Repositories
{
    public interface IJsonPlaceHolderRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<List<UsersJson>> GetAllUsersJsonPlaceholder();

    }
}
