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
        IList<AuctionDTO> GetAllAuctions();
        AuctionDTO GetAuctionById(Guid id);
        AuctionDTO Create(AuctionDTO User);
        void Update(AuctionDTO User);

        void Remove(Guid id);
    }
}
