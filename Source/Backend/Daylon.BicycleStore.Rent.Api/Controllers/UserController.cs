using Daylon.BicycleStore.Rent.Application.Interface;
using Daylon.BicycleStore.Rent.Communication.Request.User;
using Microsoft.AspNetCore.Mvc;

namespace Daylon.BicycleStore.Rent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RequestRegisterUserJson request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userDTO = await _userService.RegisterUserAsync(request);

            return Ok(userDTO);
        }
    }
}
