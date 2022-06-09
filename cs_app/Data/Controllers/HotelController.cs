using Microsoft.AspNetCore.Mvc;
using cs_app.Data.Models;
using cs_app.Data.Services;
using cs_app.Data.DTOs;

namespace cs_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly HotelService _context;

        public HotelsController(HotelService context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotels()
        {
            return await _context.GetHotels();
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<Hotel>> GetHotelsById(int id)
        {
            var hotels = await _context.GetHotel(id);

            if (hotels == null)
            {
                return NotFound();
            }

            return hotels;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Hotel>> PutHotels(int id, [FromBody] HotelDTO hotels)
        {
            var result = await _context.UpdateHotel(id, hotels);
            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotels([FromBody] HotelDTO hotels)
        {
            var result = await _context.AddHotel(hotels);
            if (result == null)
            {
                BadRequest();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotels(int id)
        {
            var hotels = await _context.DeleteHotel(id);
            if (hotels)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("incomplete")]
        public async Task<ActionResult<IEnumerable<IncompleteHotelDTO>>> GetAuthorIncomplete()
        {
            return await _context.GetHotelsIncomplete();
        }
    }
}