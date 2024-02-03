using DAL.Contracts;
using Entities.Models;


namespace DAL.Concrete
{
    internal class AuctionRepository : BaseRepository<Auction, Guid>, IAuctionRepository
    {
        public AuctionRepository(AuctionDbContext dbContext) : base(dbContext)
        {
        }
    }
}
