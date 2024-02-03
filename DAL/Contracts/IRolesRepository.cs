using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IRolesRepository : IRepository<Role, Guid>
    {
        Guid Findid(string role);
        Role Add(Role roli);
        Role GetById(Guid id);
        void Update(Role roli);
        void Remove(Guid id);
        Role GetByRoleName(string name);
        IEnumerable<Role> GetAll();
        
    }
}
