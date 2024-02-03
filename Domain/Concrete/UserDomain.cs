using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO.UserDTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;


namespace Domain.Concrete
{
    internal class UserDomain : DomainBase, IUserDomain
    {
        public UserDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }
        private IUserRepository UserRepository => _unitOfWork.GetRepository<IUserRepository>();

        public IList<UserDTO> GetAllUsers()
        {
            IEnumerable<User> user = UserRepository.GetAll();
            var test = _mapper.Map<IList<UserDTO>>(user);
            return test;
        }

        public UserDTO GetUserById(Guid id)
        {
            User user = UserRepository.GetById(id);
            return _mapper.Map<UserDTO>(user);
        }

        public UserDTO Create(UserDTO User)
        {

            User user = _mapper.Map<User>(User);
            user.UserId = Guid.NewGuid();
            
            foreach (var item in User.RoleId)
            {

                UserRole x = new UserRole();
                x.UserId = user.UserId;
                x.RoleId = item;
                user.UserRoles.Add(x);
            }

            UserRepository.Create(user);
            return _mapper.Map<UserDTO>(user);
        }

        public void Update(UserDTO User)
        {
            var updateproject = _mapper.Map<User>(User);
            UserRepository.Update(updateproject);

        }

        public void Remove(Guid id)
        {
            UserRepository.Remove(id);
        }
    }
}
