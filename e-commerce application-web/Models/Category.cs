using System.ComponentModel.DataAnnotations;

namespace e_commerce_application_web.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int DisplayOrder { get; set; }
        public string Name { get; set; }
    }
}
