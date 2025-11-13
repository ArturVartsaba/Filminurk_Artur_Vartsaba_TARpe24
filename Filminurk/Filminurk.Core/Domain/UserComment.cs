using System.ComponentModel.DataAnnotations;

namespace Filminurk.Core.Domain
{
    public class UserComment
    {
        [Key]
        public Guid CommentID { get; set; }
        public string? CommenterUserID { get; set; }
        public string CommentBody { get; set; }
        public int CommentedScore { get; set; }
        public int? IsHelpful { get; set; } //👍
        public int? IsHarmful { get; set; } //👎

        /* Andmebaasi jaoks vajalikud andmed */
        public DateTime CommentCreatedAt { get; set; }
        public DateTime CommentModifiedAt { get; set; }
        public DateTime? CommentDeletedAt { get; set; }
    }
}
