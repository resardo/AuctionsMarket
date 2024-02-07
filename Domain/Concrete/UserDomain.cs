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

        public IList<User1DTO> GetAllUsers()
        {
            IEnumerable<User> user = UserRepository.GetAll();
            var test = _mapper.Map<IList<User1DTO>>(user);
            return test;
        }

        public User1DTO GetUserById(Guid id)
        {
            User user = UserRepository.GetById(id);
            return _mapper.Map<User1DTO>(user);
        }

        public User1DTO Create(User1DTO User)
        {

            User user = _mapper.Map<User>(User);
            user.UserId = Guid.NewGuid();
            user.Wallet = 1000.00M;
            if (User.RoleId != null && User.RoleId.Count > 0 )
            {
                //if is role based app
                foreach (var item in User.RoleId)
                {

                    UserRole x = new UserRole();
                    x.UserRoleId = Guid.NewGuid();
                    x.UserId = user.UserId;
                    x.RoleId = item;
                    user.UserRoles.Add(x);
                }
            }

            else
            {
                //deafult role user
                UserRole x = new UserRole();
                x.UserRoleId = Guid.NewGuid();
                x.UserId = user.UserId;
                x.RoleId = Guid.Parse("bf5b1e8b-272b-4121-84eb-a9b69b786778");
                
            }
            UserRepository.Create(user);
            return _mapper.Map<User1DTO>(user);
        }

        public void Update(User1DTO User)
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
