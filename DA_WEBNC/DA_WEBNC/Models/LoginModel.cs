using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DA_WEBNC.Models
{
    public class LoginModel
    {
        [Display(Name ="Emaill")]
        [Required(ErrorMessage = "Không thể bỏ trống")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Không thể bỏ trống")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}