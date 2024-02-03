using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public partial class UserRole
    {
        [Key]
        public Guid UserRoleId { get; set; }

        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
