using DTO.BidDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IBidDomain
    {
        IList<BidDTO> GetAllBids();
        BidDTO GetBidById(Guid id);
        BidDTO Create(BidDTO User);
        void Update(BidDTO User);

        void Remove(Guid id);
    }
}
