using DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IUserDomain
    {
        IList<UserDTO> GetAllUsers();
        UserDTO GetUserById(Guid id);
        UserDTO Create(UserDTO User);
        void Update(UserDTO User);

        void Remove(Guid id);
    }
}
