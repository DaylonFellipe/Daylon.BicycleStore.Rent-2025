using Daylon.BicycleStore.Rent.Communication.Request;
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
        public async Task<IActionResult> GetBicyclesAsync()
        {
            var bicycles = await _bicycleService.GetBicyclesAsync();

            if (bicycles == null || bicycles.Count == 0)
                return NotFound("No bicycles found.");

            return Ok(bicycles);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBicycleByIdAsync(Guid id)
        {
            var bicycle = await _bicycleService.GetBicycleByIdAsync(id);

            if (bicycle == null)
                return NotFound($"Bicycle with ID {id} not found.");

            return Ok(bicycle);
        }

        //POST

        [HttpPost]
        public async Task<IActionResult> RegisterBicycleAsync([FromBody] RequestRegisterBicycleJson request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bicycle = await _bicycleService.RegisterBicycleAsync(request);

            return Ok(bicycle);
        }

        //PUT

        [HttpPut]
        public async Task<IActionResult> UpdateBicycleAsync([FromBody] RequestUpdateBicycleJson request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bicycle = await _bicycleService.UpdateBicycleAsync(request);

            return Ok();
        }

        //DELETE

        [HttpDelete]
        public async Task<IActionResult> DeleteBicycleAsync(Guid id)
        {
            await _bicycleService.DeleteBicycleAsync(id);

            return Ok();
        }
    }
}
