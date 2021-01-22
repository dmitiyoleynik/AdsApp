using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.Models
{
    public class Ad
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "required")]
        public AdType? Type { get; set; }
        [Required(ErrorMessage = "required")]
        public AdCategory? Category { get; set; }
        [Required(ErrorMessage = "required")]
        public float Cost { get; set; }
        [Required(ErrorMessage = "required")]
        public string Content { get; set; }
        public bool IsActive { get; set; }
        public List<string> Tags { get; set; }
    }
}
