using Filminurk.Data;
using Filminurk.Models.Actors;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class ActorsController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        public ActorsController(FilminurkTARpe24Context context)
        {
            _context = context;
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
                    Gender = (Gender)a.Gender
                }
            );
            return View(result);
        }
    }
}
