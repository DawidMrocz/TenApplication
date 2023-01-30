using Aplikacja.DTOS.UserDtos;
using Aplikacja.Entities.UserModel;
using Aplikacja.Repositories.UserRepository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Aplikacja.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            //check if the emailAddress already exists or not in database
            var user = await _userRepository.IsEmailInUse(email);
            if (user)
            {     
                return Json($"Email {email} is already in use");
            }
            else
            {
                return Json(true);
            }
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyPhone([RegularExpression(@"^\d{3}-\d{3}-\d{4}$")] string phone)
        {
            if (!ModelState.IsValid)
            {
                return Json($"Phone {phone} has an invalid format. Format: ###-###-####");
            }
            return Json(true);
        }


        [HttpGet]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<UserDto>> Profile()
        {
            try
            {
                UserDto profile = await _userRepository.GetProfile(Guid.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value));
                return View(profile);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Logout")]
        public async Task<ActionResult<bool>> Logout([FromForm] LoginDto command)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Home");
        }

        [HttpDelete]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> DeleteUser()
        {
            try
            {
                bool profileDeleted = await _userRepository.DeleteUser(Guid.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value));
                return profileDeleted;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> UpdateUser([FromBody] UpdateDto updateUser)
        {
            var userId = Guid.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            try
            {
                User newUser = await _userRepository.UpdateUser(updateUser, userId);
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
