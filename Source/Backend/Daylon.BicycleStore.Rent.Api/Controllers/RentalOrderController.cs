using Daylon.BicycleStore.Rent.Communication.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace Daylon.BicycleStore.Rent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalOrderController : ControllerBase
    {
        //private readonly Interface nomaDaInterface

        public RentalOrderController()
        {
           
        }

        [HttpPost]
        public async Task<IActionResult> RegisterRentalOrderAsync([FromBody] RequestRegisterRentalOrderJson request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var rentalOrder = 0; //*await _service.execute(request);*/

            return Ok(rentalOrder);
        }



    }
}
