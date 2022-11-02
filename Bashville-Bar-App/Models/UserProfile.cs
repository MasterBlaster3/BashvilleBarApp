using System;
using System.ComponentModel.DataAnnotations;

namespace BashvilleBarApp.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        [StringLength(28, MinimumLength = 28)]
        public string FirebaseUserId { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(255)]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        
        public int? ImageId { get; set; }
        
        public bool IsAdmin { get; set; }
        
        public DateTime CreateDateTime { get; set; }
    }
}
