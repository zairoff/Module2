using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Task1.Models;

namespace Task1.ViewModels
{
    public class ProductViewModel
    {
        [Required]

        public Product Product { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<Supplier> Suppliers { get; set; }
    }
}
