using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectA.Models
{
    public class AddressModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }
        [ForeignKey("ClientModel")] 
        public int ClientId { get; set; }
        [Required]
        [StringLength(20)] 
        public string Province { get; set; }
        [Required]
        [StringLength(20)] 
        public string District { get; set; }
        [Required]
        [StringLength(20)] 
        public string Ward { get; set; }
        [Required]
        [StringLength(255)]
        public string Street { get; set; }
        [Required] 
        public AddressType AddressType { get; set; }
        public bool Default { get; set; }
        // Navigation property
         public virtual ClientModel Client { get; set; } } 
         public enum AddressType { office, home }
    }

