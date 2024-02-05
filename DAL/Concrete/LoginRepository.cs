using DAL.Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Concrete
{
    internal class LoginRepository : BaseRepository<User, Guid>, ILoginRepository
    {


        public LoginRepository(AuctionDbContext dbContext) : base(dbContext)
        {
        }

        public User Generate(User User)
        {
            var login = context.Include(x => x.UserRoles).ThenInclude(x => x.Role).Where(a => a.Username == User.Username && a.Password == User.Password).FirstOrDefault();
            return login ?? null;
        }
    }
}
