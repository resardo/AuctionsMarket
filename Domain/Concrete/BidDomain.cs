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
        private IBidRepository _bidRepository => _unitOfWork.GetRepository<IBidRepository>();
        private IAuctionRepository _auctionRepository => _unitOfWork.GetRepository<IAuctionRepository>();
        private IUserRepository _userRepository => _unitOfWork.GetRepository<IUserRepository>();



        public BidDTO Create(BidDTO bid)
        {
            var user = _userRepository.GetById(bid.UserId);
            var auction = _auctionRepository.GetById(bid.AuctionId);
            

            if (auction == null || user == null)
            {
                throw new Exception("Auction or User does not exist.");
            }

            if (auction.EndTime <= DateTime.UtcNow)
            {
                throw new Exception("Auction has already ended.");
            }

            if (auction.StartPrice >= bid.Amount)
            {
                throw new Exception("Bid must be higher than start price");
            }

            var highestBid =  _bidRepository.GetHighestBid(bid.AuctionId);

            if (highestBid != null && bid.Amount <= highestBid.Amount)
            {
                throw new Exception("Bid must be higher than the current highest bid.");
            }

            if (user.Wallet < bid.Amount)
            {
                throw new Exception("Insufficient funds to place bid.");
            }

            bid.BidId = Guid.NewGuid();
            var bidEntity = _mapper.Map<Bid>(bid);
            return _mapper.Map<BidDTO>(_bidRepository.Create(bidEntity));


        }

        public IList<BidDTO> GetAllBids()
        {
            IEnumerable<Bid> bid = _bidRepository.GetAll();
            var test = _mapper.Map<IList<BidDTO>>(bid);
            return test;
        }

        public BidDTO GetBidById(Guid id)
        {
            Bid bid = _bidRepository.GetById(id);
            return _mapper.Map<BidDTO>(bid);
        }

        public BidDTO GetHighesBidById(Guid id)
        {
            Bid bid = _bidRepository.GetHighestBid(id);
            return _mapper.Map<BidDTO>(bid);
        }

        public void Remove(Guid id)
        {
            _bidRepository.Remove(id);
        }

        public void Update(BidDTO Bid)
        {
            var bidUpdate = _mapper.Map<Bid>(Bid);
            _bidRepository.Update(bidUpdate);
        }
    }
}
