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
        private IAuctionRepository _auctionRepository => _unitOfWork.GetRepository<IAuctionRepository>();
        private IBidRepository _bidRepository => _unitOfWork.GetRepository<IBidRepository>();
        private IUserRepository _userRepository => _unitOfWork.GetRepository<IUserRepository>();

        public void CloseAuction(Guid auctionId)
        {
            var auction =  _auctionRepository.GetById(auctionId);
            //if (auction == null || auction.EndTime > DateTime.UtcNow)
            //{
            //    throw new Exception("Auction does not exist or has not yet ended.");
            //}

            var highestBid = _bidRepository.GetHighestBid(auctionId);
            if (highestBid == null)
            {
                // No bids were placed, no wallet updates needed
                return;
            }

            BuyerTransaction(highestBid);
            SellerTransaction(auction, highestBid.Amount);
           

            auction.SoldPrice = highestBid.Amount;
            auction.IsActive = false;

            _auctionRepository.Update(auction);
             
        }

        public void BuyerTransaction(Bid highestBid)
        {
            var buyer = _userRepository.GetById(highestBid.UserId);
            buyer.Wallet -= highestBid.Amount;
            _userRepository.Update(buyer);
        }
        public void SellerTransaction(Auction auction,decimal highestBid)
        {
            var seller = _userRepository.GetById(auction.UserId);
            seller.Wallet += highestBid;
            _userRepository.Update(seller);
        }

        public AuctionDTO Create(AuctionDTO auction)
        {
            auction.AuctionId = Guid.NewGuid();
            auction.IsActive = true;
            //auction.IsActive = true;
            var bid = _mapper.Map<Auction>(auction);
            var auctionEntity = _auctionRepository.Create(bid);
            return _mapper.Map<AuctionDTO>(auctionEntity);
        }

        public IEnumerable<AuctionDTO> GetAllAuctions()
        {
            IEnumerable<Auction> auction = _auctionRepository.GetAuctionsBasedOnTimeRemaining();
            var test = _mapper.Map<IEnumerable<AuctionDTO>>(auction);
            return test;
        }

        public AuctionDTO GetAuctionById(Guid id)
        {
            Auction auction = _auctionRepository.GetById(id);
            return _mapper.Map<AuctionDTO>(auction);
        }

        public void Remove(Guid id)
        {
            _auctionRepository.Remove(id);
        }

        public void Update(AuctionDTO auction)
        {
            var auctionUpdate = _mapper.Map<Auction>(auction);
            _auctionRepository.Update(auctionUpdate);
        }

    }
}
