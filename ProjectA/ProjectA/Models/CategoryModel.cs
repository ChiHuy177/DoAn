using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectA.Models
{
    public class CategoryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }
        [Required][StringLength(50)] 
        public string Name { get; set; }
        [StringLength(255)] 
        public string Description { get; set; } 

        public int ParentID { get; set; }
        // Navigation property
        public virtual ICollection<ProductModel> Products { get; set; }
    }
}
