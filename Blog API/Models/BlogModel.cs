
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog_API.Models
{
    public class BlogModel
    {
      

        [Key]
        public int Id { get; set; }

        public string Slug { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Body { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<CommentModel> Comments { get; set; }
        public ICollection<Blog_TagModel> Blog_Tag { get; set; }

       
        
    }
}
