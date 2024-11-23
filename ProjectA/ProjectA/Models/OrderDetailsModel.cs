using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectA.Models
{
    public class OrderDetailsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }
        [ForeignKey("OrderModel")] 
        public int OrderId { get; set; }
        [ForeignKey("ProductModel")] 
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(15,2)")] 
        public decimal Price { get; set; } 
        // Navigation properties
        public virtual OrderModel Order { get; set; } 
        public virtual ProductModel Product { get; set; }
    }
}
