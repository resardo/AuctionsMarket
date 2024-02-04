using DAL.Contracts;
using Entities.Models;


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
            var currentTime = DateTime.UtcNow; 
            return  context.Where(a => a.IsActive && a.EndTime > currentTime) 
                                 .OrderBy(a => a.EndTime) 
                                 .ToList();
        }
    }
}
