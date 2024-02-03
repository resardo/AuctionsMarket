using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public partial class User
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string PasswordHash { get; set; }

        public decimal Wallet { get; set; } = 1000.00M;

        // Navigation properties
        public virtual ICollection<Auction> Auctions { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }

}
