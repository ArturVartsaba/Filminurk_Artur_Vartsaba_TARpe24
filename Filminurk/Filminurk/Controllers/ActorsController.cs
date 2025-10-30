using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Filminurk.Models.Actors;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class ActorsController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IActorServices _actorServices;
        public ActorsController
            (
            FilminurkTARpe24Context context,
            IActorServices actorServices
            )
        {
            _context = context;
            _actorServices = actorServices;
        }
        public IActionResult Index()
        {
            var result = _context.Actors
                .Select(a => new ActorsIndexViewModel
                {
                    ActorID = a.ActorID,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Age = a.Age,
                    CountryOfOrigin = a.CountryOfOrigin,
                    Gender = (Models.Actors.Gender)a.Gender
                }
            );
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ActorsCreateUpdateViewModel result = new();
            return View("CreateUpdate", result);
        }

        public IActorServices Get_actorServices()
        {
            return _actorServices;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ActorsCreateUpdateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var dto = new ActorDTO()
                {
                    ActorID = vm.ActorID,
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    NickName = vm.NickName,
                    MoviesActedFor = vm.MoviesActedFor,
                    PortraitID = vm.PortraitID,
                    Age = vm.Age,
                    CountryOfOrigin = vm.CountryOfOrigin,
                    Gender = (Core.Dto.Gender)vm.Gender
                };
                var result = await _actorServices.Create(dto);
                if (result == null)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
