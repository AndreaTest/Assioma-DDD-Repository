using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Assioma.Ecomm.Domain;

namespace Assioma.Ecomm.Presentation.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Use short names, please.")]
        public string Name { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Price must be between 1 and 1000 euros.")]
        public decimal Price { get; set; }
    }
}
