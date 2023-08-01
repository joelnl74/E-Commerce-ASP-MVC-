using System.ComponentModel.DataAnnotations;

namespace e_commerce_application_web.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Range(1, 100)]
        public int DisplayOrder { get; set; }
        [MaxLength(32)]
        public string Name { get; set; }
    }
}
