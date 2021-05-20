using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entities
{
    public class Candy
    {
        [HiddenInput(DisplayValue = false)]
        public int CandyId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please, input the Name")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Please, input the Description")]
        public string Description { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Please, input the Category")]
        public string Category { get; set; }

        [Display(Name = "Price (RUB)")]
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please, input the positive value")]
        public decimal Price { get; set; }

        [Display(Name = "Weight (gr)")]
        [Required(ErrorMessage = "Please, input the Weight")]
        public decimal Weight { get; set; }
    }
}
