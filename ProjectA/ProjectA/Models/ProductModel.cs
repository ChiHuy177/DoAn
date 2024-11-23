using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectA.Models
{
    public class ProductModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [Column(TypeName = "TEXT")]
        public string Description { get; set; }

        [Column(TypeName = "TEXT")]
        public string ShortDescription { get; set; }

        public int InStock { get; set; }

        [Required]
        [Column(TypeName = "decimal(15,2)")]
        public decimal Price { get; set; }

        [StringLength(100)]
        public string Unit { get; set; }

        [StringLength(255)]
        public string Image { get; set; }

        public virtual CategoryModel Category { get; set; }
    }
}
