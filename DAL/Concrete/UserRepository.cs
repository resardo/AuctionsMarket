using DAL.Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;


namespace DAL.Concrete
{
    internal class UserRepository : BaseRepository<User, Guid>, IUserRepository
    {

        public UserRepository(AuctionDbContext dbContext) : base(dbContext)
        {
        }
  
        public void Create(User User)
        {
            context.Add(User);
            db.SaveChanges();
        }

        public void Update(User User)
        {
            if (db.Entry(User).State == EntityState.Detached)
                context.Attach(User);
            
            //context.Update(project);
            SetModified(User);
            PersistChangesToTrackedEntities();
        }


        public User GetById(Guid id)
        {
            var user = context.Where(a => a.UserId == id).FirstOrDefault();
            return user;
        }

        public User GetByActionId(Guid id)
        {
            var user = context
                       .Include(u => u.Auctions) 
                       .FirstOrDefault(u => u.Auctions.Any(a => a.AuctionId == id)); 
            return user;
        }
        public void Remove(Guid id)
        {
            User emp = context.Find(id);
            if (emp != null)
                Remove(emp);
            
            PersistChangesToTrackedEntities();
        }

    }

}
