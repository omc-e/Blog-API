using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog_API.Models
{
    [Table("customer", Schema ="dbo")]
    public class Blog_TagModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Column("BlogId")]
        public int BlogId { get; set; }
        public BlogModel? Blog { get; set; }

        [Column("TagId")]
        public int TagId { get; set; }      
        public TagModel? Tag { get; set; }
        
    }
}
