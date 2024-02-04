﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.AuctionDTO
{
    public class AuctionDTO
    {
        
        public Guid AuctionId { get; set; }

       
        public Guid UserId { get; set; }


        public string Title { get; set; } 

        
        public string Description { get; set; }

        
        public DateTime StartTime { get; set; }

        
        public DateTime EndTime { get; set; }

        
        public decimal StartPrice { get; set; }

        public bool IsActive { get; set; } = null!;
    }
}
