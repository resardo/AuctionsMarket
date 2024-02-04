using Domain.Contracts;
using DTO.BidDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuctionsMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private IConfiguration _config;
        private readonly IBidDomain _bidDomain;

        public BidController(IConfiguration config, IBidDomain bidDomain)
        {
            _config = config;
            _bidDomain = bidDomain;
        }

        [HttpGet]
        [Route("getAllbids")]
        public IActionResult GetAllBids()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var bids = _bidDomain.GetAllBids();
                return (bids != null) ? Ok(bids) : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpGet]
        [Route("{bidId}")]
        public IActionResult GetBidById([FromRoute] Guid bidId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var bid = _bidDomain.GetBidById(bidId);
                return (bid != null) ? Ok(bid) : NotFound();

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpPost]
        [Route("Register")]
        public IActionResult Register(BidDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                else
                {
                    _bidDomain.Create(request);
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

        public IActionResult Update([FromBody] BidDTO bid)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                else
                {
                    _bidDomain.Update(bid);
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
        public IActionResult DeleteBid(Guid Id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                _bidDomain.Remove(Id);
                return Ok("update completed");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
