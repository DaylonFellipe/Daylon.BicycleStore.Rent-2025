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

        [HttpGet]
        public async Task<IList<Bicycle>> GetBicyclesAsync()
        {
            var result = await _bicycleService.GetBicyclesAsync();

            return result;

        }
    }
}
