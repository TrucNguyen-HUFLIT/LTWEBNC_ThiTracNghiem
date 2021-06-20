using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DA_WEBNC.Models
{
    public partial class TypeAcc
    {
        public TypeAcc()
        {
            HocSinhs = new HashSet<HocSinh>();
            NhanViens = new HashSet<NhanVien>();
        }

        public string Idtype { get; set; }
        public string Name { get; set; }

        public virtual ICollection<HocSinh> HocSinhs { get; set; }
        public virtual ICollection<NhanVien> NhanViens { get; set; }
    }
}