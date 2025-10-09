namespace Filminurk.Models.Movies
{
    public class MoviesIndexViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateOnly FirstPublished { get; set; }
        public double? CurrentRating { get; set; }

        /* 3 õpilase valitud andmetüüpi */
        public string? Genre { get; set; }
        public string? Language { get; set; }
        public int? DurationInMinutes { get; set; }
    }
}
