using DAL.Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    internal class BidRepository : BaseRepository<Bid, Guid>, IBidRepository
    {


        public BidRepository(AuctionDbContext dbContext) : base(dbContext)
        {
        }

    
    }
}
