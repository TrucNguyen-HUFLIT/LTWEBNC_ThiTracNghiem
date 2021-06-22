using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DA_WEBNC.Models
{
    public class ExamTestViewModel
    {
        public string IDBaiThiHS { get; set; }
        public string IDStudent { get; set; }
        public BaiThi BaiThi { get; set; }
        public CauHoi CauHoi { get; set; }
    }
}