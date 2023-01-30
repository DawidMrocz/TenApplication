using Aplikacja.DTOS.CatDto;
using Aplikacja.DTOS.UserDtos;
using Aplikacja.Entities.CatModels;
using Aplikacja.Repositories.CatRepository;
using Aplikacja.Repositories.UserRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aplikacja.Controllers
{
    [Authorize]
    public class CatController : Controller
    {
        private readonly ICatRepository _catRepository;
        private readonly IMapper _mapper;

        public CatController(ICatRepository catRepository, IMapper mapper)
        {
            _catRepository = catRepository ?? throw new ArgumentNullException(nameof(catRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<UserDto>> Cat(Guid id)
        {
            var myCat = _mapper.Map<UserDto>(await _catRepository.GetCat(id));
            return View(myCat);
        }

        [HttpGet]
        public async Task<ActionResult<List<CatRecordDTO>>> Cats()
        {
            var myCats = _mapper.Map<List<CatRecordDTO>>(await _catRepository.GetCats());
            return View(myCats);
        }
    }
}
