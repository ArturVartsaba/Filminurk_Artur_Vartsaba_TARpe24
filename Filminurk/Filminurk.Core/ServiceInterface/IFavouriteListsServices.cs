using Filminurk.Core.Domain;
using Filminurk.Core.Dto;

namespace Filminurk.Core.ServiceInterface
{
    public interface IFavouriteListsServices
    {
        public Task<FavouriteList> DetailsAsync(Guid id);
        public async Task<FavouriteList> Create(FavouriteListDTO dto, List<Movie> selectedMovies);
    }
}
