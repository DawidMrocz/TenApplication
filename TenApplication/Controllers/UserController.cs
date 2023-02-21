using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenApplication.Repositories;
using System.Security.Claims;
using TenApplication.Dtos;
using TenApplication.Dtos.DesignerDTOModels;
using TenApplication.Models;
using TenApplication.ActionFilters;

namespace TenApplication.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepository _UserRepository;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository UserRepository, ILogger<UserController> logger)
        {
            _UserRepository = UserRepository ?? throw new ArgumentNullException(nameof(UserRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<UserDto>> Profile()
        {
            try
            {
                UserDto profile = await _UserRepository.GetProfile(Guid.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value));
                return View(profile);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationFilter(DTOName = "model")]
        public async Task<IActionResult> Register([FromBody][Bind(include: "UserName,Email,Phone,Password,CCtr,ActTyp,ProfilePhoto")] RegisterDto model)
        {
            try
            {
                await _UserRepository.CreateUser(model);
                return RedirectToAction("Login", "Access");
            }
            catch
            {
                _logger.LogInformation("Process not succeed!");
                return View();
            }
        }

        public async Task<ActionResult<bool>> Logout()
        {
            try
            {
                await _UserRepository.LogOut();
                return RedirectToAction("Login", "Access");
            }
            catch
            {
                _logger.LogInformation("Process not succeed!");
                return View();
            }
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteUser()
        {
            try
            {
                bool profileDeleted = await _UserRepository.DeleteUser(Guid.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value));
                return profileDeleted;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<User>> UpdateUser([FromBody] UpdateDto updateUser, [FromRoute]Guid userId)
        {
            var UserId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
            try
            {
                User newUser = await _UserRepository.UpdateUser(updateUser,userId);
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
