using Daylon.BicycleStore.Rent.Application.Interface;
using Daylon.BicycleStore.Rent.Application.Services.Bicycles;
using Daylon.BicycleStore.Rent.Communication.Request.User;
using Daylon.BicycleStore.Rent.Domain.Entity.Enum;
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

        // GET
        [HttpGet]
        public async Task<IActionResult> GetUsersAsync(UserStatusFilterEnum filterEnum)
        {
            var users = await _userService.GetUsersAsync(filterEnum);

            if (users == null || !users.Any())
                return NotFound("No users found.");

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
                return NotFound($"User with ID {id} not found.");

            return Ok(user);
        }

        [HttpGet("NameOrEmail")]
        public async Task<IActionResult> GetUsersByNameOrEmailAsync(string nameOrEmail)
        {
            if (string.IsNullOrWhiteSpace(nameOrEmail))
                return BadRequest("Name or Email parameter is required.");

            if (nameOrEmail.Length <= 2)
                return BadRequest("Name or Email parameter must be longer than 2 characters.");

            var users = await _userService.GetUserByNameOrEmailAsync(nameOrEmail);

            if (users == null)
                return NotFound($"User with Name or Email '{nameOrEmail}' not found.");

            return Ok(users);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RequestRegisterUserJson request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userDTO = await _userService.RegisterUserAsync(request);

            return Ok(userDTO);
        }

        // PATCH
        [HttpPatch("Name")]
        public async Task<IActionResult> UpdateUserNameAsync(Guid id, string? firstName, string? LastName)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userDTO = await _userService.UpdateUserNameAsync(id, firstName!, LastName!);

            return Ok(userDTO);
        }

        // PUT
        [HttpPut("ChangeUserStatus")]
        public async Task<IActionResult> UpdateUserStatusAsync(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userService.UpdateUserStatusAsync(id);

            return Ok($"User Status Update to {user.Active}");
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _userService.DeleteUserAsync(id);

            return NoContent();
        }
    }
}
