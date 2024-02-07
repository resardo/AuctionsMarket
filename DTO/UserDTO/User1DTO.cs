using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserDTO
{
    public class User1DTO
    {

        public Guid? UserId { get; set; } 

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public decimal Wallet { get; set; } = 1000.00M;

        public List<Guid>? RoleId { get; set; }
    }
}
