using Filminurk.Core.Domain;
using Filminurk.Core.Dto;

namespace Filminurk.Core.ServiceInterface
{
    public interface IFilesServices
    {
        void FilesToApi(MoviesDTO dto, Movie domain);
        Task<FileToApi> RemoveImageFromApi(FileToApiDTO dto);
        Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDTO[] dtos);
    }
}