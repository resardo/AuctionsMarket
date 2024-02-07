using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IAuctionRepository : IRepository<Auction, Guid>
    {
        Auction Create(Auction auction);
        void Update(Auction auction);
        void Remove(Guid id);
        IEnumerable<Auction> GetAuctionsBasedOnTimeRemaining();
        IEnumerable<Auction> GetAuctionsToClose();

    }
}
