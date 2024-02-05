using DAL.Contracts;
using Entities.Models;
using Helpers.Methods;
using Microsoft.EntityFrameworkCore;

namespace DAL.Concrete
{
    internal class AuctionRepository : BaseRepository<Auction, Guid>, IAuctionRepository
    {
        public AuctionRepository(AuctionDbContext dbContext) : base(dbContext)
        {

        }

        public Auction Create(Auction auction)
        {
            context.Add(auction);
            PersistChangesToTrackedEntities();

            return context.Add(auction).Entity;
        }

        public IEnumerable<Auction> GetAuctionsBasedOnTimeRemaining()
        {
            var currentTime = HelperMethods.GetCurrentDate();
            return context.Include(x => x.User).ThenInclude(x => x.Bids).Where(a => a.IsAvailable && a.EndTime > currentTime) 
                                 .OrderBy(a => a.EndTime) 
                                 .ToList();
        }
    }
}
