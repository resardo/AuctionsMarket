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
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; } 

        public decimal Wallet { get; set; }

        // Navigation properties
        public virtual ICollection<Auction> Auctions { get; set; } = new List<Auction>();
        public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }

}
