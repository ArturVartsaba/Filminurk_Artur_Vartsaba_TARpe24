using System.ComponentModel.DataAnnotations;

namespace Filminurk.Core.Domain
{
    public class FileToApi
    {
        [Key]
        public Guid ImageID { get; set; }
        public string? ExistingFilePath { get; set; }
        public Guid? MovieID { get; set; }
        public bool? IsPoster { get; set; } //määrab ära kas pilt on poster või mitte
    }
}