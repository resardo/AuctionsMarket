using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO.BidDTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    internal class BidDomain : DomainBase, IBidDomain
    {
        public BidDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }
        private IBidRepository BidRepository => _unitOfWork.GetRepository<IBidRepository>();

        public BidDTO Create(BidDTO Bid)
        {
            Bid.BidId = Guid.NewGuid();
            var bid = _mapper.Map<Bid>(Bid);
            return _mapper.Map<BidDTO>(BidRepository.Add(bid));


        }

        public IList<BidDTO> GetAllBids()
        {
            IEnumerable<Bid> bid = BidRepository.GetAll();
            var test = _mapper.Map<IList<BidDTO>>(bid);
            return test;
        }

        public BidDTO GetBidById(Guid id)
        {
            Bid bid = BidRepository.GetById(id);
            return _mapper.Map<BidDTO>(bid);
        }

        public void Remove(Guid id)
        {
            BidRepository.Remove(id);
        }

        public void Update(BidDTO Bid)
        {
            var bidUpdate = _mapper.Map<Bid>(Bid);
            BidRepository.Update(bidUpdate);
        }
    }
}
