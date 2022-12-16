
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog_API.Models
{
    [Table("Blogs", Schema ="dbo")]
    public class BlogModel
    {
      

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("Slug")]
        public string Slug { get; set; }

        [Required]
        [Column("Title")]
        public string Title { get; set; }
        [Required]
        [Column("Description")]
        public string Description { get; set; }
        [Required]
        [Column("Body")]
        public string Body { get; set; }
        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }
        [Column("UpdatedAt")]
        public DateTime UpdatedAt { get; set; }

        public List<CommentModel> Comments { get; set; }
        public ICollection<Blog_TagModel> Blog_Tag { get; set; }

       
        
    }
}
