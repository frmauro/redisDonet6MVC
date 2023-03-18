
using dotnet.Models; 
namespace dotnet.Repository;

public interface IUserRepository
    {
        Task<List<User>> GetUsersAsync();
    }