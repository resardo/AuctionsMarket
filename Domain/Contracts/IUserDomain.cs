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
        IList<User1DTO> GetAllUsers();
        User1DTO GetUserById(Guid id);
        User1DTO Create(User1DTO User);
        void Update(User1DTO User);

        void Remove(Guid id);
    }
}
