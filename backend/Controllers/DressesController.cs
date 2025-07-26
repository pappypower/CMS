using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeddingDressCMS.API.Models;
using WeddingDressCMS.API.Services;

namespace WeddingDressCMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DressesController : ControllerBase
    {
        private readonly IDressService _dressService;

        public DressesController(IDressService dressService)
        {
            _dressService = dressService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeddingDress>>> GetDresses([FromQuery] string? search, [FromQuery] int? categoryId)
        {
            try
            {
                if (!string.IsNullOrEmpty(search))
                {
                    var searchResults = await _dressService.SearchDressesAsync(search);
                    return Ok(searchResults);
                }
                
                if (categoryId.HasValue)
                {
                    var categoryDresses = await _dressService.GetDressesByCategoryAsync(categoryId.Value);
                    return Ok(categoryDresses);
                }

                var dresses = await _dressService.GetAllDressesAsync();
                return Ok(dresses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching dresses.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WeddingDress>> GetDress(int id)
        {
            try
            {
                var dress = await _dressService.GetDressByIdAsync(id);
                if (dress == null)
                {
                    return NotFound(new { message = $"Dress with ID {id} not found." });
                }
                return Ok(dress);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching the dress.", error = ex.Message });
            }
        }

        [HttpGet("featured")]
        public async Task<ActionResult<IEnumerable<WeddingDress>>> GetFeaturedDresses()
        {
            try
            {
                var featuredDresses = await _dressService.GetFeaturedDressesAsync();
                return Ok(featuredDresses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching featured dresses.", error = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<WeddingDress>> CreateDress([FromBody] WeddingDress dress)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdDress = await _dressService.CreateDressAsync(dress);
                return CreatedAtAction(nameof(GetDress), new { id = createdDress.Id }, createdDress);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the dress.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<WeddingDress>> UpdateDress(int id, [FromBody] WeddingDress dress)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedDress = await _dressService.UpdateDressAsync(id, dress);
                if (updatedDress == null)
                {
                    return NotFound(new { message = $"Dress with ID {id} not found." });
                }

                return Ok(updatedDress);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the dress.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> DeleteDress(int id)
        {
            try
            {
                var result = await _dressService.DeleteDressAsync(id);
                if (!result)
                {
                    return NotFound(new { message = $"Dress with ID {id} not found." });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the dress.", error = ex.Message });
            }
        }
    }
} 