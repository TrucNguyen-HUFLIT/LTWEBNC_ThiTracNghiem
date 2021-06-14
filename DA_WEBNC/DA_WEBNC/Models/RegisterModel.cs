using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DA_WEBNC.Models
{
    public class RegisterModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Không thể bỏ trống")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Không thể bỏ trống")]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        [Display(Name = "ConfirmPassword")]
        [Required(ErrorMessage = "Không thể bỏ trống")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Không trùng khớp với Password")]
        public string ConfirmPassword { get; set; }

    }
}