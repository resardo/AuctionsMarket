
using Entities.Models;


namespace DAL.Contracts
{
    public interface ILoginRepository : IRepository<User, Guid>
    {
       User Generate(User emp);
    }
}
