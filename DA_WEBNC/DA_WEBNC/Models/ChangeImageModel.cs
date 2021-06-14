using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DA_WEBNC.Models
{
    public class ChangeImageModel
    {
        public string ID { get; set; }
        public string Avatar { get; set; }
        public IFormFile UploadAvt { get; set; }
    }
}