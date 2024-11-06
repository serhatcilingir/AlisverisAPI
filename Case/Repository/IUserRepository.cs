using System.Threading.Tasks;
using Case.Models;
using Case.Repositories;


namespace Case.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }
}
