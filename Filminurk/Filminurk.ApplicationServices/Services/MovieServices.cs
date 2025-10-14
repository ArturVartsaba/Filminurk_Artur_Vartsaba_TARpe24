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

        public MovieServices(FilminurkTARpe24Context context) 
        { 
            _context = context;
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
            //movie.EntryCreatedAt = DateTime.Now;
            //movie.EntryModifiedAt = DateTime.Now;

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            return movie;
        }
        public async Task<Movie> DetailsAsync(Guid id) 
        { 
            var result = await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
    }
}
