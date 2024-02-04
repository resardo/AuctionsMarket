using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public partial class Bid
    {
        [Key]
        public Guid BidId { get; set; }

        [Required]
        public Guid AuctionId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        // Navigation properties
        [ForeignKey("AuctionId")]
        public virtual Auction Auction { get; set; } 

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }

}
