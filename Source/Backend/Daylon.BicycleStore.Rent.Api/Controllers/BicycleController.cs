using Daylon.BicycleStore.Rent.Communication.Request;
using Daylon.BicycleStore.Rent.Domain.Entity;
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
            var bicycles= await _bicycleService.GetBicyclesAsync();
            
            if(bicycles == null || bicycles.Count == 0)
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

        //PUT

        //DELETE

    }
}
