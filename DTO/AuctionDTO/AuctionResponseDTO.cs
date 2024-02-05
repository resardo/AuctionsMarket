using DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.AuctionDTO
{
    public class AuctionResponseDTO
    {
        public Guid AuctionId { get; set; }


        public Guid UserId { get; set; }


        public string Title { get; set; }


        public string Description { get; set; }


        public DateTime StartTime { get; set; }


        public DateTime EndTime { get; set; }


        public decimal StartPrice { get; set; }

        public bool IsAvailable { get; set; } 

        public User1DTO User { get; set; }


    }
    
}
