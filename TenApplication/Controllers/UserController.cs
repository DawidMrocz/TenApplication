using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenApplication.Repositories;
using System.Security.Claims;
using TenApplication.Models;
using TenApplication.Dtos.DesignerDTOModels;
using TenApplication.Dtos;

namespace TenApplication.Controllers
{
    [Authorize]
    public class DesignerController : Controller
    {
        private readonly IDesignerRepository _DesignerRepository;

        public DesignerController(IDesignerRepository DesignerRepository)
        {
            _DesignerRepository = DesignerRepository ?? throw new ArgumentNullException(nameof(DesignerRepository));
        }

        [HttpGet]
        public async Task<ActionResult<DesignerDto>> Profile()
        {
            try
            {
                DesignerDto profile = await _DesignerRepository.GetProfile(int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value));
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
        public async Task<ActionResult<bool>> DeleteDesigner()
        {
            try
            {
                bool profileDeleted = await _DesignerRepository.DeleteDesigner(int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value));
                return profileDeleted;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Designer>> UpdateDesigner([FromBody] UpdateDto updateDesigner, [FromRoute]int userId)
        {
            var DesignerId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            try
            {
                Designer newDesigner = await _DesignerRepository.UpdateDesigner(updateDesigner,userId);
                return Ok(newDesigner);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
