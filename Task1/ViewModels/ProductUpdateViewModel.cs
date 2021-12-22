using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Task1.Models;

namespace Task1.ViewModels
{
    public class ProductUpdateViewModel
    {
        [Required]

        public ProductViewModel Product { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public IEnumerable<SupplierViewModel> Suppliers { get; set; }
    }
}
