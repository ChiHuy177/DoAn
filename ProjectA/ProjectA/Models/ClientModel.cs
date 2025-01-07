using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectA.Models
{
    public class ClientModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [StringLength(255)]
        public string Password { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public DateTime Dob { get; set; }
        [Required]
        [StringLength(10)]
        public string Phone { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        public byte Role { get; set; }

        public void SetPassword(string password)
        {
            this.Password = BCrypt.Net.BCrypt.HashPassword(password);
        }
        public bool VerifyPassword(string password) { 
            return BCrypt.Net.BCrypt.Verify(password, this.Password); 
        }
    }
}
