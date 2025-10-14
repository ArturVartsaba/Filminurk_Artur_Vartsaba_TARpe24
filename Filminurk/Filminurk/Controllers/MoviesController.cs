using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Filminurk.Models.Movies;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class MoviesController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IMovieServices _movieServices;
        public MoviesController
            (
            FilminurkTARpe24Context context,
            IMovieServices movieServices
            )
        {
            _context = context;
            _movieServices = movieServices;
        }
        public IActionResult Index()
        {
            var result = _context.Movies.Select(x => new MoviesIndexViewModel
            {
                Id = x.Id,
                Title = x.Title,
                FirstPublished = x.FirstPublished,
                CurrentRating = x.CurrentRating,
                Genre = x.Genre,
                Language = x.Language,
                DurationInMinutes = x.DurationInMinutes
            });
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            MoviesCreateViewModel result = new();
            return View("Create", result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(MoviesCreateViewModel vm) 
        {
            var dto = new MoviesDTO()
            {
                Id = vm.Id,
                Title = vm.Title,
                Description = vm.Description,
                FirstPublished = vm.FirstPublished,
                CurrentRating = vm.CurrentRating,
                Director = vm.Director,
                Actors = vm.Actors,
                Genre = vm.Genre,
                Language = vm.Language,
                DurationInMinutes = vm.DurationInMinutes,
                EntryCreatedAt = vm.EntryCreatedAt,
                EntryModifiedAt = vm.EntryModifiedAt
            };
            var result = await _movieServices.Create(dto);
            if (result == null) 
            { 
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
