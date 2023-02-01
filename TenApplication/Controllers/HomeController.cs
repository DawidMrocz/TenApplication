using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using TenApplication.Models;
using TenApplication.Repositories;

namespace TenApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork= unitOfWork;
        }

        public async Task<ActionResult<IEnumerable<Job>>> Index()
        {
            IEnumerable<Job>? jobs = await _unitOfWork.Jobs.GetAll();
            return View(jobs);
        }
   
        public async Task<ActionResult<Job>> Details(int id)
        {
            Job? job = await _unitOfWork.Jobs.GetById(j => j.JobId == id);
            return View(job);
        }

        public async Task<ActionResult<Job>> Details2(int id)
        {
            Job? job = await _unitOfWork.Jobs.GetById(j => j.JobId == id);
            return View(job);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}