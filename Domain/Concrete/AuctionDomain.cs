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
using Helpers.Methods;
using Microsoft.Extensions.Hosting;
using Serilog;
using AuctionsMarket.Exceptions;

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
            var auction = _auctionRepository.GetById(auctionId);
            if (auction == null || auction.EndTime > HelperMethods.GetCurrentDate())
            {
                throw new TransactionException("Auction does not exist or has not yet ended.");
            }

            var highestBid = _bidRepository.GetHighestBid(auctionId);
            if (highestBid == null)
            {
                auction.FinalPrice = auction.StartPrice;
                auction.IsAvailable = false;
                _auctionRepository.Update(auction);
                return;
            }

            
            bool transactionSuccess = BuyerTransaction(highestBid);
            if (transactionSuccess)
            {
                SellerTransaction(auction, highestBid.Amount);

                auction.FinalPrice = highestBid.Amount;
                auction.IsAvailable = false;
                _auctionRepository.Update(auction);
            }
            else
            {
                HandleFailedTransaction(auction, highestBid);
            }
        }

        private bool BuyerTransaction(Bid highestBid)
        {
            var buyer = _userRepository.GetById(highestBid.UserId);
            if (buyer.Wallet >= highestBid.Amount)
            {
                buyer.Wallet -= highestBid.Amount;
                _userRepository.Update(buyer);
                Log.Information("Buyer Transaction => {@buyer.Wallet}", buyer.Wallet);
                return true; 
            }
            else
            {
                
                Log.Information("Buyer does not have enough funds. Transaction failed for {@buyer.UserName}", buyer.Username);
                return false; 
            }
        }

        private void SellerTransaction(Auction auction, decimal highestBid)
        {
            var seller = _userRepository.GetById(auction.UserId);
            seller.Wallet += highestBid;
            _userRepository.Update(seller);
        }

        private void HandleFailedTransaction(Auction auction, Bid highestBid)
        {
            auction.IsAvailable = false;
            _auctionRepository.Update(auction);
            Log.Information("Transaction failed for Auction {@auction} with highest bid {@highestBid}", auction, highestBid);
        }

        public AuctionDTO Create(AuctionDTO auction)
        {
            auction.AuctionId = Guid.NewGuid();
            auction.IsAvailable = true;
            var bid = _mapper.Map<Auction>(auction);
            var auctionEntity = _auctionRepository.Create(bid);
            return _mapper.Map<AuctionDTO>(auctionEntity);
        }

        public IEnumerable<AuctionResponseDTO> GetAllAuctions()
        {
            IEnumerable<Auction> auction = _auctionRepository.GetAuctionsBasedOnTimeRemaining();
            var test = _mapper.Map<IEnumerable<AuctionResponseDTO>>(auction);
            return test;
        }

        public AuctionDTO GetAuctionById(Guid id)
        {
            Auction auction = _auctionRepository.GetById(id);
            return _mapper.Map<AuctionDTO>(auction);
        }

        public void Remove(Guid id)
        {
            Auction auction = _auctionRepository.GetById(id);
            auction.IsAvailable = false;
            _auctionRepository.Update(auction);
        }

        public void Update(AuctionDTO auction)
        {
            var auctionUpdate = _mapper.Map<Auction>(auction);
            _auctionRepository.Update(auctionUpdate);
        }

        public IEnumerable<AuctionResponseDTO> GetAuctionsToClose()
        {
            IEnumerable<Auction> auction = _auctionRepository.GetAuctionsToClose();
            var test = _mapper.Map<IEnumerable<AuctionResponseDTO>>(auction);
            return test;
        }
    }
}
