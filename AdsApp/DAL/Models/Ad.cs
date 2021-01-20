using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Ad
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "required")]
        public AdType Type { get; set; }
        [Required(ErrorMessage = "required")]
        public Category Category { get; set; }
        [Required(ErrorMessage = "required")]
        public float Cost { get; set; }
        [Required(ErrorMessage = "required")]
        public string Content { get; set; }
        public bool IsActive { get; set; }
    }
}
