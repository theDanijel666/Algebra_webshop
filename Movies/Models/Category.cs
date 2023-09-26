using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Movies.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength (200, MinimumLength =2)]
        public string Title { get; set; }

        [ForeignKey("CategoryId")]
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
