using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog_API.Models
{
    public class TagModel
    {
      
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TagId { get; set; }


        public string TagName { get; set; }

        public ICollection<Blog_TagModel> Blog_Tag { get; set; }



    }
}
