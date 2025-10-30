namespace Filminurk.Models.Movies
{
    public class MoviesUpdateViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly FirstPublished { get; set; }
        public string Director { get; set; }
        public List<string>? Actors { get; set; }
        public double? CurrentRating { get; set; }
        //public List<UserComment>? Reviews { get; set; }

        /* Kassaasolevate piltide andmeomadused */
        public List<IFormFile> Files { get; set; }
        public List<ImageViewModel> Images { get; set; } = new List<ImageViewModel>();

        /* 3 õpilase valitud andmetüüpi */
        public string? Genre { get; set; }
        public string? Language { get; set; }
        public int? DurationInMinutes { get; set; }

        /* Andmebaasi jaoks vajalikud */
        public DateTime? EntryCreatedAt { get; set; }
        public DateTime? EntryModifiedAt { get; set; }
    }
}
