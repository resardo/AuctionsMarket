using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public partial class Auction
    {
        [Key]
        public Guid AuctionId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public decimal StartPrice { get; set; }

        public decimal SoldPrice { get; set; }   

        public bool IsActive { get; set; }  
        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();
    }

}
