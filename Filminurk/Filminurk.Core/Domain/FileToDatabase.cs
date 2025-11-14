using System.ComponentModel.DataAnnotations;

namespace Filminurk.Core.Domain
{
    public class FileToDatabase
    {
        [Key]
        public Guid ImageID { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public string Image { get; set; }
        public Guid? ListID { get; set; }
    }
}
