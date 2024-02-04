using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserDTO
{
    public class UserDTO
    {
       
        public Guid UserId { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public decimal Wallet { get; set; } = 1000.00M;

        public List<Guid> RoleId { get; set; }
    }
}
