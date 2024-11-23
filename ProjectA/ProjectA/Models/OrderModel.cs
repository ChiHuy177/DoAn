using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectA.Models
{
    public class OrderModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }
        [ForeignKey("ClientModel")] 
        public int ClientId { get; set; }
        [ForeignKey("AddressModel")]
        [StringLength(255)] 
        public string AddressId { get; set; }
        [Required] 
        public DateTime OrderDate { get; set; }
        [Required]
        [Column(TypeName = "decimal(15,2)")] 
        public decimal TotalValue { get; set; }
        [StringLength(10)] 
        public string DeliveryStatus { get; set; }
        [StringLength(255)] 
        public string Note { get; set; }
        [StringLength(20)] 
        public string PaymentMethod { get; set; }
        [StringLength(20)]
        public string PaymentStatus { get; set; } // Navigation properties
        public virtual ClientModel Client { get; set; } 
        public virtual AddressModel Address { get; set; }
    }
}
