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

        public void Update(Auction auction)
        {
            if (db.Entry(auction).State == EntityState.Detached)
            {
                context.Attach(auction);
            }
            //context.Update(project);
            SetModified(auction);
            PersistChangesToTrackedEntities();
        }

        public IEnumerable<Auction> GetAuctionsBasedOnTimeRemaining()
        {
            var currentTime = HelperMethods.GetCurrentDate();
            return context.Include(x => x.User).ThenInclude(x => x.Bids).Where(a => a.IsAvailable && a.EndTime > currentTime) 
                                 .OrderBy(a => a.EndTime) 
                                 .ToList();
        }



        public void Remove(Guid id)
        {
            Auction auction = context.Find(id);
            if (auction != null)
                Remove(auction);

            PersistChangesToTrackedEntities();
        }

        public IEnumerable<Auction> GetAuctionsToClose()
        {
            var currentTime = HelperMethods.GetCurrentDate();
            return context.Where(a => a.EndTime <= currentTime && a.IsAvailable).ToList();

        }
    }
}
