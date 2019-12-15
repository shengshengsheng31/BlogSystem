using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogSystem.MVCSite.Models.UserViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength:16,MinimumLength =6)]
        public string Password { get; set;}
        [Required]
        [Compare(otherProperty:nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}