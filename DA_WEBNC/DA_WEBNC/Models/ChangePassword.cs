using System.ComponentModel.DataAnnotations;

namespace DA_WEBNC.Models
{
    public class ChangePassword
    {
        [Display(Name = "ID Nhân viên")]
        public string IDNhanVien { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu hiện tại")]
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu mới")]
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string NewPassword { get; set; }

        [Compare("NewPassword")]
        [Display(Name = "Nhập lại mật khẩu")]
        [Required(ErrorMessage = "Không được bỏ trống")]
        [DataType(DataType.Password)]
        public string ConfirmPass { get; set; }
    }
}