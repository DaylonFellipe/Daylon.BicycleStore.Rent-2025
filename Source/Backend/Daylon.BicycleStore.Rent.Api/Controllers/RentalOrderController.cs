using Daylon.BicycleStore.Rent.Application.Interface;
using Daylon.BicycleStore.Rent.Application.Services.Bicycles;
using Daylon.BicycleStore.Rent.Communication.Request;
using Daylon.BicycleStore.Rent.Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

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
    }
}
