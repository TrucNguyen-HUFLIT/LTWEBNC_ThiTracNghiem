using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DA_WEBNC.Models
{
    public class ViewModelNV
    {
        public NhanVien nhanVien { get; set; }
        public IPagedList<NhanVien> ListNhanViens { get; set; }
        public NhanVien[] ListNhanVien { get; set; }
        public TypeAcc[] ListType { get; set; }

        public ChangePassword changePass { get; set; }
    }
}