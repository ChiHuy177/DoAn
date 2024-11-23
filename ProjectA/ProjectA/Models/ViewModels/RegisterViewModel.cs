using System.ComponentModel.DataAnnotations;

namespace ProjectA.Models.ViewModels
{
    public class RegisterViewModel
    {
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
    }
}
