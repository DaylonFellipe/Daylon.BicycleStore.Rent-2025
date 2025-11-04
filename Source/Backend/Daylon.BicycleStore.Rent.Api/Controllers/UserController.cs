using Daylon.BicycleStore.Rent.Application.Interface;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUsersAsync(UserStatusFilterEnum filterEnum)
        {
            var users = await _userService.GetUsersAsync(filterEnum);

            return Ok(users);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserByIdAsync(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            return Ok(user);
        }

        [HttpGet("nameOrEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUsersByNameOrEmailAsync(string nameOrEmail)
        {
                var users = await _userService.GetUserByNameOrEmailAsync(nameOrEmail);

                return Ok(users);
        }

        // POST
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RequestRegisterUserJson request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userDTO = await _userService.RegisterUserAsync(request);

            return Created("", userDTO);
        }

        // PATCH
        [HttpPatch("name")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserNameAsync(Guid id, string? firstName, string? LastName)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userDTO = await _userService.UpdateUserNameAsync(id, firstName!, LastName!);

            return Ok(userDTO);
        }

        [HttpPatch("email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserEmailAsync(Guid id, string newEmail, string password)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userDTO = await _userService.UpdateUserEmailAsync(id, newEmail, password);

            return Ok(userDTO);
        }

        [HttpPatch("password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserPasswordAsync(Guid id, string oldPassword, string newPassword)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userDTO = await _userService.UpdateUserPasswordAsync(id, oldPassword, newPassword);

            return Ok(userDTO);
        }

        [HttpPatch("BirthDate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserBirthdayDateAsync(Guid id, DateTime birthdayDate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userDTO = await _userService.UpdateUserBirthdayDateAsync(id, birthdayDate);

            return Ok(userDTO);
        }

        // PUT
        [HttpPut("changeUserStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserStatusAsync(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userService.UpdateUserStatusAsync(id);

            return Ok($"User Status Update to {user.Active}");
        }

        // DELETE
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var user = await _userService.GetUserByIdAsync(id);

            await _userService.DeleteUserAsync(id);

            return NoContent();
        }
    }
}

//   /ᐠ - ˕ -マ D A Y L O N