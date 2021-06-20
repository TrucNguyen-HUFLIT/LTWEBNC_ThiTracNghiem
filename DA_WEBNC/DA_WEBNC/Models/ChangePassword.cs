using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DA_WEBNC.Models
{
    public class ChangePassword
    {
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu hiện tại")]
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu mới")]
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string NewPassword { get; set; }

        [NotMapped]
        [Compare("NewPassword")]
        [Display(Name = "Nhập lại mật khẩu")]
        [Required(ErrorMessage = "Không được bỏ trống")]
        [DataType(DataType.Password)]
        public string ConfirmPass { get; set; }
        public object OldPassword { get; internal set; }
    }
}