using DTO.RoleDTO;
using Entities.Models;


namespace Domain.Contracts
{
    public interface IRoleDomain
    {
        RoleDTO1 CreateRole(RoleDTO1 roli);
        RoleDTO1 GetByRoleName(string name);
        RoleDTO1 GetById(Guid id);
        void Remove(Guid id);
        IList<RoleDTO1> GetAllRoles();
        void Update(RoleDTO1 role);
    }
}
