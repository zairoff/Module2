using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task1.ViewModels.Product
{
    public class ProductView
    {
        public int? ProductID { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public int SupplierID { get; set; }

        [Required]
        public int CategoryID { get; set; }

        public string QuantityPerUnit { get; set; }

        public decimal UnitPrice { get; set; }

        public short UnitsInStock { get; set; }

        public short UnitsOnOrder { get; set; }

        public short ReorderLevel { get; set; }

        public bool Discontinued { get; set; }
    }
}
