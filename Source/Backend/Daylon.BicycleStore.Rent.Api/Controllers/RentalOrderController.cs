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
        public async Task<IActionResult> GetRentalOrdersAsync()
        {
            var rentalOrders = await _rentalOrderService.GetRentalOrdersAsync();

            if (rentalOrders == null || rentalOrders.Count == 0)
                return NotFound("No rental orders found.");

            return Ok(rentalOrders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRentalOrderByIdAsync(Guid id)
        {
            var rentalOrder = await _rentalOrderService.GetRentalOrderByIdAsync(id);

            if (rentalOrder == null)
                return NotFound($"Rental order with ID {id} not found.");

            return Ok(rentalOrder);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> RegisterRentalOrderAsync([FromBody] RequestRegisterRentalOrderJson request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var rentalOrder = await _rentalOrderService.RegisterRentalOrderAsync(request);

            return Ok(rentalOrder);
        }

        // PATCH
        [HttpPatch("DateProperties")]
        public async Task<IActionResult> ModifyDatesAsync(Guid id, DateTime? rentalStart, int? rentalDays, int? extraDays)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var rentalOrder = await _rentalOrderService.ModifyDatesAsync(id, rentalStart, rentalDays, extraDays);

            return Ok(rentalOrder);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRentalOrderAsync(Guid id)
        {
            await _rentalOrderService.DeleteRentalOrderAsync(id);

            return NoContent();
        }
    }
}
