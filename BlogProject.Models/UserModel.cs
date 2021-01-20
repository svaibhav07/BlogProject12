using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="User Name")]
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        [Display(Name = "Is Active")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "The field Is Active must be checked.")]
        public bool IsActive { get; set; }

        [Display(Name = "Is Admin")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "The field Is Active must be checked.")]
        public bool IsAdmin { get; set; }


    }
}
