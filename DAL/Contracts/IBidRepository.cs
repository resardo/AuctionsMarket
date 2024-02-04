using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IBidRepository : IRepository<Bid, Guid>
    {
        //Bid GetBidByActionId(Guid auctionId);
        Bid Create(Bid bid);
        Bid GetHighestBid(Guid auctionId);
    }
}
