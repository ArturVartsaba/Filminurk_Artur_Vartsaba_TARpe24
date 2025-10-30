using Filminurk.Core.Domain;
using Filminurk.Core.Dto;

namespace Filminurk.Core.ServiceInterface
{
    public interface IMovieServices // see on interface. asub .Core/ServiceInterface
    {
        Task<Movie> Create(MoviesDTO dto);
        Task<Movie> Delete(Guid id);
        Task<Movie> DetailsAsync(Guid id);
        Task<Movie> Update(MoviesDTO dto);
    }
}