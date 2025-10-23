using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Filminurk.Core.Dto
{
    public class MoviesDTO
    {
        public Guid? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateOnly? FirstPublished { get; set; }
        public string? Director { get; set; }
        public List<string>? Actors { get; set; }
        public double? CurrentRating { get; set; }
        //public List<UserComment>? Reviews { get; set; }

        /* Kassaasolevate piltide andmeomadused */
        public List<IFormFile> Files { get; set; }
        public IEnumerable<FileToApiDTO> FileToApiDTOs { get; set; } = new List<FileToApiDTO>();

        public string? Genre { get; set; }
        public string? Language { get; set; }
        public int? DurationInMinutes { get; set; }
        
        public DateTime? EntryCreatedAt { get; set; }
        public DateTime? EntryModifiedAt { get; set; }
    }
}
