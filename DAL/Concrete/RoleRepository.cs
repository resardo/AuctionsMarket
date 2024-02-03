using DAL.Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;


namespace DAL.Concrete
{
    internal class RoleRepository : BaseRepository<Role, Guid>, IRolesRepository
    {
        public RoleRepository(AuctionDbContext dbContext) : base(dbContext)
        {
        }
        
        public Role GetByRoleName(string name)
        {
            var roli = context.Where(a => a.Name == name).FirstOrDefault();
            return roli;
        }
        public Role GetById(Guid id)
        {
            var roli = context.Where(a => a.RoleId == id).FirstOrDefault();
            return roli;
        }

        public Role Add(Role roli)
        {
            context.Add(roli);
            PersistChangesToTrackedEntities();
           
            return context.Add(roli).Entity;
        }

        public void Update(Role roli)
        {
            if (db.Entry(roli).State == EntityState.Detached)
            {
                context.Attach(roli);
            }
            //context.Update(project);
            SetModified(roli);
            PersistChangesToTrackedEntities();
        }

        public void Remove(Guid id)
        {
            Role RoleToRemove = context.Find(id);
            if (RoleToRemove != null)
            {
                Remove(RoleToRemove);
            }
            PersistChangesToTrackedEntities();
        }

        public IEnumerable<Role> GetAll()
        {
            return context.ToList();
        }

        public Guid Findid(string role)
        {
            var s = context.Find(role);
            return s.RoleId;
        }
    }
}
