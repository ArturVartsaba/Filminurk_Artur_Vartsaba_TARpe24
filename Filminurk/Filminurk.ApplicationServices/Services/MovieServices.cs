using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.ApplicationServices.Services
{
    internal class MovieServices : IMovieServices
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IFilesServices _filesServices;

        public MovieServices
            (
            FilminurkTARpe24Context context,
            IFilesServices fileServices  // failid          
            ) 
        { 
            _context = context;
            _filesServices = fileServices; // failid
        }

        public async Task<Movie> Create(MoviesDTO dto)
        {
            Movie movie = new Movie();
            movie.Id = Guid.NewGuid();
            movie.Title = dto.Title;
            movie.Description = dto.Description;
            movie.CurrentRating = dto.CurrentRating;
            movie.Genre = dto.Genre;
            movie.Language = dto.Language;
            movie.DurationInMinutes = dto.DurationInMinutes;
            movie.FirstPublished = (DateOnly)dto.FirstPublished;
            movie.Director = dto.Director;
            movie.Actors = dto.Actors;
            movie.EntryCreatedAt = DateTime.Now;
            movie.EntryModifiedAt = DateTime.Now;
            _filesServices.FileToApi(dto, movie);

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            return movie;
        }
        public async Task<Movie> DetailsAsync(Guid id) 
        { 
            var result = await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
        public async Task<Movie> Update(MoviesDTO dto)
        {
            Movie movie = new Movie();

            movie.Id = Guid.NewGuid();
            movie.Title = dto.Title;
            movie.Description = dto.Description;
            movie.CurrentRating = dto.CurrentRating;
            movie.Genre = dto.Genre;
            movie.Language = dto.Language;
            movie.DurationInMinutes = dto.DurationInMinutes;
            movie.FirstPublished = (DateOnly)dto.FirstPublished;
            movie.Director = dto.Director;
            movie.Actors = dto.Actors;
            movie.EntryCreatedAt = DateTime.Now;
            movie.EntryModifiedAt = DateTime.Now;
            _filesServices.FileToApi(dto, movie);

            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();

            return movie;
        }

        public async Task<Movie> Delete(Guid id)
        {
            var result = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);

            var images = await _context.FilesToApi
                .Where(x => x.MovieID == id)
                .Select(y => new FileToApiDTO
                {
                    ImageID = y.ImageID,
                    MovieID = y.MovieID,
                    FilePath = y.ExistingFilePath
                }).ToArrayAsync();

            await _filesServices.RemoveImageFromApi(images);
            _context.Movies.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}
