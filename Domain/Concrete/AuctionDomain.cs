using AutoMapper;
using DAL.UoW;
using Domain.Contracts;
using Microsoft.AspNetCore.Http;
using DAL.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.AuctionDTO;
using Entities.Models;

namespace Domain.Concrete
{
    internal class AuctionDomain : DomainBase, IAuctionDomain
    {
        public AuctionDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }
        private IAuctionRepository AuctionRepository => _unitOfWork.GetRepository<IAuctionRepository>();

        public AuctionDTO Create(AuctionDTO auction)
        {
            auction.AuctionId = Guid.NewGuid();
            var bid = _mapper.Map<Auction>(auction);
            return _mapper.Map<AuctionDTO>(AuctionRepository.Add(bid));
        }

        public IList<AuctionDTO> GetAllAuctions()
        {
            IEnumerable<Auction> auction = AuctionRepository.GetAll();
            var test = _mapper.Map<IList<AuctionDTO>>(auction);
            return test;
        }

        public AuctionDTO GetAuctionById(Guid id)
        {
            Auction auction = AuctionRepository.GetById(id);
            return _mapper.Map<AuctionDTO>(auction);
        }

        public void Remove(Guid id)
        {
            AuctionRepository.Remove(id);
        }

        public void Update(AuctionDTO auction)
        {
            var auctionUpdate = _mapper.Map<Auction>(auction);
            AuctionRepository.Update(auctionUpdate);
        }
    }
}
