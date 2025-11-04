using Daylon.BicycleStore.Rent.Application.Interface;
using Daylon.BicycleStore.Rent.Communication.Request;
using Microsoft.AspNetCore.Mvc;

namespace Daylon.BicycleStore.Rent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalOrderController : ControllerBase
    {
        private readonly IRentalOrderService _rentalOrderService;

        public RentalOrderController(IRentalOrderService rentalOrderService)
        {
            _rentalOrderService = rentalOrderService;
        }

        // GET
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRentalOrdersAsync()
        {
            var rentalOrders = await _rentalOrderService.GetRentalOrdersAsync();

            return Ok(rentalOrders);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRentalOrderByIdAsync(Guid id)
        {
            var rentalOrder = await _rentalOrderService.GetRentalOrderByIdAsync(id);

            return Ok(rentalOrder);
        }

        // POST
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterRentalOrderAsync([FromBody] RequestRegisterRentalOrderJson request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var rentalOrder = await _rentalOrderService.RegisterRentalOrderAsync(request);

            return Created("", rentalOrder);
        }

        // PATCH
        [HttpPatch("DateProperties")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ModifyDatesAsync(Guid id, DateTime? rentalStart, int? rentalDays, int? extraDays)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var rentalOrder = await _rentalOrderService.ModifyDatesAsync(id, rentalStart, rentalDays, extraDays);

            return Ok(rentalOrder);
        }

        // DELETE
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRentalOrderAsync(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var rentalOrder = await _rentalOrderService.GetRentalOrderByIdAsync(id);

            await _rentalOrderService.DeleteRentalOrderAsync(id);

            return NoContent();
        }
    }
}

//   /ᐠ - ˕ -マ D A Y L O N