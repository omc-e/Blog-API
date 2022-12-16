using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog_API.Models
{
    public class CommentModel
    {
        [Key]
        public int CommentId { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Body { get; set; }
        public int BlogId { get; set; }
       
        public BlogModel? Blog { get; set; }
    }
}
