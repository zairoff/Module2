using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task1.ViewModels
{
    public class CategoryView
    {
        public int CategoryID { get; set; }

        [Required]
        public string CategoryName { get; set; }

        [Required]
        public string Description { get; set; }

        public byte[] Picture { get; set; }

        public IFormFile FormFile { get; set; }
    }
}
