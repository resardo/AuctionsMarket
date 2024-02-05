using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.BidDTO
{
    public class BidDTO
    {
        
        public Guid BidId { get; set; }

        public Guid AuctionId { get; set; }

        public Guid UserId { get; set; }

        public decimal Amount { get; set; }

        public DateTime Timestamp { get; set; }

    }
}
