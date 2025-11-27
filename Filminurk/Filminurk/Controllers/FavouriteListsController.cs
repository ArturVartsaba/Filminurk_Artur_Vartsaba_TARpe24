using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Filminurk.Models.FavouriteLists;
using Filminurk.Models.Movies;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class FavouriteListsController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IFavouriteListsServices _FavouriteListsServices;
        //fileService missing
        public FavouriteListsController(FilminurkTARpe24Context context, IFavouriteListsServices favouriteListsServices)
        {
            _context = context;
            _FavouriteListsServices = favouriteListsServices;
        }
        public IActionResult Index()
        {
            var resultingLists = _context.FavouriteLists
                .OrderByDescending(y => y.ListCreatedAt) /* sorteeri nimekiri langevas järjekorras loodud kuupäeva järgi */
                .Select(x => new FavouriteListsIndexViewModel
                {
                    FavouriteListID = x.FavouriteListID,
                    ListBelongsToUser = x.ListBelongsToUser,
                    IsMovieOrActor = x.IsMovieOrActor,
                    ListName = x.ListName,
                    ListDescription = x.ListDescription,
                    ListCreatedAt = x.ListCreatedAt,
                    Image = (List<FavouriteListsIndexImageViewModel>)_context.FilesToDatabase
                        .Where(ml => ml.ListID == x.FavouriteListID)
                        .Select(li => new FavouriteListsIndexImageViewModel
                        {
                            ListID = li.ListID,
                            ImageID = li.ImageID,
                            ImageData = li.ImageData,
                            ImageTitle = li.ImageTitle,
                            Image = string.Format("data:image/gif;base64,{0}",
                            Convert.ToBase64String(li.ImageData)),
                        })

                });
            return View(resultingLists);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var movies = _context.Movies
                .OrderBy(m => m.Title)
                .Select(mo => new MoviesIndexViewModel
                {
                    Id = mo.Id,
                    Title = mo.Title,
                    FirstPublished = mo.FirstPublished,
                    Genre = mo.Genre,
                })
                .ToList();
            ViewData["allmovies"] = movies;
            ViewData["UserHasSelected"] = new List<string>();

            // this is for normal user
            FavouriteListsUserCreateViewModel vm = new();
            return View("UserCreate", vm);
        }
        [HttpPost]
        public async Task<IActionResult> UserCreate(FavouriteListsUserCreateViewModel vm, List<string> userHasSelected,
            List<MoviesIndexViewModel> movies)
        {
            List<Guid> tempParse = new();
            foreach (var stringID in userHasSelected)
            {
                tempParse.Add(Guid.Parse(stringID));
            }

            var newListDto = new FavouriteListDTO() { };
            newListDto.ListName = vm.ListName;
            newListDto.ListDescription = vm.ListDescription;
            newListDto.IsMovieOrActor = vm.IsMovieOrActor;
            newListDto.IsPrivate = vm.IsPrivate;
            newListDto.ListCreatedAt = DateTime.UtcNow;
            newListDto.ListBelongsToUser = "00000000-0000-0000-0000-000000000001";
            newListDto.ListModifiedAt = DateTime.UtcNow;
            newListDto.ListDeletedAt = vm.ListDeletedAt;

            var listofmoviestoadd = new List<Movie>();
            foreach (var movieId in tempParse)
            {
                Movie thismovie = (Movie)_context.Movies.Where(m => m.Id == movieId).ToList().First();
                listofmoviestoadd.Add(thismovie);

            }
            newListDto.ListOfMovies = listofmoviestoadd;

            List<Guid> convertedIDs = new List<Guid>();
            if (newListDto.ListOfMovies != null)
            {
                convertedIDs = MovieToId(newListDto.ListOfMovies);
            }
            var newList = await _FavouriteListsServices.Create(newListDto /*, convertedIDs*/);
            if (newList == null)
            {
                return BadRequest();
            }
            return RedirectToAction("Index", vm);
        }

        [HttpGet]
        public async Task<IActionResult> UserDetails(Guid id, Guid thisuserid)
        {
            if (id == null || thisuserid == null) 
            {
                return BadRequest();
                //TODO: return corresponding errorviews. id not found for list, and user login error for userid
            }
            var thisList = _context.FavouriteLists
                .Where(tl => tl.FavouriteListID == id && tl.ListBelongsToUser == thisuserid.ToString())
                .Select(
                stl => new FavouriteListsUserDetailsViewModel
                {
                    FavouriteListID = stl.FavouriteListID,
                    ListBelongsToUser = stl.ListBelongsToUser,
                    IsMovieOrActor = stl.IsMovieOrActor,
                    ListName = stl.ListName,
                    ListDescription = stl.ListDescription,
                    IsPrivate = stl.IsPrivate,
                    ListOfMovies = stl.ListOfMovies,
                    IsReported = stl.IsReported,
                    //Image = _context.FilesToDatabase
                    //.Where(i => i.ListID == stl.FavouriteListID)
                    //.Select(si => new FavouriteListsIndexImageViewModel
                    //{
                    //    ImageID = si.ImageID,
                    //    ListID = si.ListID,
                    //    ImageData = si.ImageData,
                    //    ImageTitle = si.ImageTitle,
                    //    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(si.ImageData))
                    //}).ToList()
                }).First();
            //add viewdata attribute here later, to discern between user and admin
            if (thisList == null) 
            { 
                return NotFound();
            }
            return View("Details", thisList);
        }

        [HttpPost]
        public IActionResult UserTogglePrivacy(Guid id) 
        { 
            FavouriteList thisList = _FavouriteListsServices.DetailsAsync(id);

            FavouriteListDTO updatedList = new FavouriteListDTO();
            updatedList.FavouriteListID = thisList.FavouriteListID;
            updatedList.ListBelongsToUser = thisList.ListBelongsToUser;
            updatedList.ListName = thisList.ListName;
            updatedList.ListDescription = thisList.ListDescription;
            updatedList.IsPrivate = thisList.IsPrivate;
            updatedList.ListOfMovies = thisList.ListOfMovies;
            updatedList.IsReported = thisList.IsReported;
            updatedList.IsMovieOrActor = thisList.IsMovieOrActor;
            updatedList.ListCreatedAt = thisList.ListCreatedAt;
            updatedList.ListModifiedAt = DateTime.UtcNow;
            updatedList.ListDeletedAt = thisList.ListDeletedAt;
            
            thisList.IsPrivate = !thisList.IsPrivate;
            _FavouriteListsServices.Update(thisList);
            return View("Details");
        }

        private List<Guid> MovieToId(List<Movie> listOfMovies)
        {
            var result = new List<Guid>();
            foreach (var movie in listOfMovies)
            {
                result.Add(movie.Id);
            }
            return result;
        }
    }
}
