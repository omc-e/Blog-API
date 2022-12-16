using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog_API.Models
{
    [Table("Comments", Schema ="dbo")]
    public class CommentModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }

        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }
        [Column("UpdatedAt")]
        public DateTime UpdatedAt { get; set; }
        [Column("Body")]
        public string Body { get; set; }
        [Column("BlogId")]
        public int BlogId { get; set; }
       
        public BlogModel? Blog { get; set; }
    }
}
