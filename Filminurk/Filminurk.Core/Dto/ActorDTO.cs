using System.ComponentModel.DataAnnotations;

namespace Filminurk.Core.Dto
{
    public enum Gender
    {
        Male,
        Female
    }
    public class ActorDTO
    {
        [Key]
        public Guid ActorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? NickName { get; set; }
        public List<string>? MoviesActedFor { get; set; }
        public int? PortraitID { get; set; }

        public int Age { get; set; }
        public string CountryOfOrigin { get; set; }
        public Gender Gender { get; set; }
    }
}
