using Microsoft.AspNetCore.Mvc;
using TenApplication.Models;
using TenApplication.Repositories;
using System.Net;
using System.Security.Claims;
using TenApplication.Dtos.DesignerDTOModels;
using TenApplication.Dtos;

namespace TenApplication.Controllers
{
    public class AccessController : Controller
    {

        private readonly IDesignerRepository _userRepository;
        private readonly IHttpContextAccessor _contxt;
        public AccessController(IDesignerRepository userRepository, IHttpContextAccessor contxt)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _contxt = contxt;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Login([FromForm] LoginDto command)
        {
            bool logInProccess = await _userRepository.LoginDesigner(command);

            if (logInProccess is false)
            {
                ViewData["validatemessage"] = "user not found";
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<ActionResult> Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity!.IsAuthenticated) return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterDto command)
        {
            if (!ModelState.IsValid) throw new BadHttpRequestException("Bad data");
            try
            {
                Designer newUser = await _userRepository.CreateDesigner(command);
                return RedirectToAction("Login", "Access");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Register()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
