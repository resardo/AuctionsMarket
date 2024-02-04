using DTO.AuctionDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IAuctionDomain
    {
        IEnumerable<AuctionDTO> GetAllAuctions();
        AuctionDTO GetAuctionById(Guid id);
        AuctionDTO Create(AuctionDTO auction);
        void Update(AuctionDTO auction);
        void CloseAuction(Guid id);
        void Remove(Guid id);
    }
}
