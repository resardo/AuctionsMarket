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

        public Bid Create(Bid bid)
        {
            context.Add(bid);
            PersistChangesToTrackedEntities();

            return context.Add(bid).Entity;
        }

        public Bid GetHighestBid(Guid auctionId)
        {
            return context.Where(b => b.AuctionId == auctionId).OrderByDescending(b => b.Amount).FirstOrDefault();
        }
    }
}
