using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.Core.Domain
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly FirstPublished { get; set; }
        public string Director { get; set; }
        public List<string>? Actors { get; set; }
        public double? CurrentRating { get; set; }
        //public List<UserComment>? Reviews { get; set; }

        /* 3 õpilase valitud andmetüüpi */
        public string? Genre { get; set; }
        public string? Language { get; set; }
        public int? DurationInMinutes { get; set; }

    }
}
