using AutoMapper;
using DAL.UoW;
using Domain.Contracts;
using DTO.RoleDTO;
using Microsoft.AspNetCore.Http;
using DAL.Contracts;
using Entities.Models;

namespace Domain.Concrete
{
    internal class RoleDomain : DomainBase, IRoleDomain
    {
        public RoleDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        private IRolesRepository roleRepository => _unitOfWork.GetRepository<IRolesRepository>();

        public IList<RoleDTO1> GetAllRoles()
        {
            IEnumerable<Role> user = roleRepository.GetAll();
            var test = _mapper.Map<IList<RoleDTO1>>(user);

            return test;
        }
        
        public RoleDTO1 CreateRole(RoleDTO1 roli)
        {

            roli.RoleId = Guid.NewGuid();
            var roleObject = _mapper.Map<Role>(roli);
            
            var returnedRole = roleRepository.Add(roleObject);
            return _mapper.Map<RoleDTO1>(returnedRole);
            
        }
        
        public RoleDTO1 GetByRoleName(string name)
        {
            var roli = roleRepository.GetByRoleName(name);
            return _mapper.Map<RoleDTO1>(roli);
        }

        public RoleDTO1 GetById(Guid id)
        {
            var roli = roleRepository.GetById(id);
            return _mapper.Map<RoleDTO1>(roli);
        }

        public void Remove(Guid id)
        {
            roleRepository.Remove(id);
        }

        public void Update(RoleDTO1 role)
        {
            var p = _mapper.Map<RoleDTO1, Role>(role);
            roleRepository.Update(p);
        }

    }
}
