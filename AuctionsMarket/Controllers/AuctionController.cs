using Domain.Contracts;
using DTO.AuctionDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuctionsMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private IConfiguration _config;
        private readonly IAuctionDomain _auctionDomain;

        public AuctionController(IConfiguration config, IAuctionDomain auctionDomain)
        {
            _config = config;
            _auctionDomain = auctionDomain;
        }

        [HttpGet]
        [Route("getAllAuctions")]
        public IActionResult GetAllAuctions()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var Auctions = _auctionDomain.GetAllAuctions();
                return (Auctions != null) ? Ok(Auctions) : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpGet]
        [Route("{AuctionId}")]
        public IActionResult GetAuctionById([FromRoute] Guid AuctionId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var Auction = _auctionDomain.GetAuctionById(AuctionId);
                return (Auction != null) ? Ok(Auction) : NotFound();

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpPost]
        [Route("Register")]
        public IActionResult Register(AuctionDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                else
                {
                    _auctionDomain.Create(request);
                    return Ok(request);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        [HttpPut]
        [Route("Update")]

        public IActionResult Update([FromBody] AuctionDTO Auction)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                else
                {
                    _auctionDomain.Update(Auction);
                    return Ok("updated");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete]
        [Route("{Id}")]
        public IActionResult DeleteAuction(Guid Id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                _auctionDomain.Remove(Id);
                return Ok("update completed");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
