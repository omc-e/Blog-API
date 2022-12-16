using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog_API.Models
{
    [Table("Tags", Schema ="dbo")]
    public class TagModel
    {
      
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TagId { get; set; }

        [Column("tag_name")]
        public string TagName { get; set; }

        public ICollection<Blog_TagModel> Blog_Tag { get; set; }



    }
}
