﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenApplication.Repositories;
using System.Security.Claims;
using TenApplication.Dtos;
using TenApplication.Dtos.DesignerDTOModels;
using TenApplication.Models;

namespace TenApplication.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepository _UserRepository;

        public UserController(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository ?? throw new ArgumentNullException(nameof(UserRepository));
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

        [Route("Logout")]
        public async Task<ActionResult<bool>> Logout([FromForm] LoginDto command)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
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
