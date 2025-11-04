using Daylon.BicycleStore.Rent.Communication.Request.Bibycle;
using Microsoft.AspNetCore.Mvc;

namespace Daylon.BicycleStore.Rent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BicycleController : ControllerBase
    {
        private readonly Application.Interface.IBicycleService _bicycleService;

        public BicycleController(Application.Interface.IBicycleService bicycleService)
        {
            _bicycleService = bicycleService;
        }
       
        //GET

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBicyclesAsync()
        {
            var bicycles = await _bicycleService.GetBicyclesAsync();

            return Ok(bicycles);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBicycleByIdAsync(Guid id)
        {
            var bicycle = await _bicycleService.GetBicycleByIdAsync(id);

            return Ok(bicycle);
        }

        //POST

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterBicycleAsync([FromBody] RequestRegisterBicycleJson request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bicycle = await _bicycleService.RegisterBicycleAsync(request);

            return Created("", bicycle);
        }

        //PUT

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBicycleAsync([FromBody] RequestUpdateBicycleJson request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bicycle = await _bicycleService.UpdateBicycleAsync(request);

            return Ok();
        }

        // PATCH

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBicyclePartialAsync(
            Guid id,
            string? name,
            string? description,
            Domain.Entity.Enum.BrandEnum? brand,
            Domain.Entity.Enum.ModelEnum? model,
            Domain.Entity.Enum.ColorEnum? color,
            double? price,
            int? quantity,
            double? dailyRate
            )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bicycle = await _bicycleService.PatchBicyclePartialAsync(id, name, description, brand, model, color, price, quantity, dailyRate);

            return Ok(bicycle);
        }

        //DELETE

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBicycleAsync(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bicycle = await _bicycleService.GetBicycleByIdAsync(id);

            await _bicycleService.DeleteBicycleAsync(id);

            return Ok();
        }
    }
}

//   /ᐠ - ˕ -マ D A Y L O N