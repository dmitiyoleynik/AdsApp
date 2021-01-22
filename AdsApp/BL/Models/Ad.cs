using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.Models
{
    public class Ad
    {
        public int Id { get; set; }
        [Required(ErrorMessage = Constants.Messages.RequiredField)]
        public AdType? Type { get; set; }
        [Required(ErrorMessage = Constants.Messages.RequiredField)]
        public AdCategory? Category { get; set; }
        [Required(ErrorMessage = Constants.Messages.RequiredField)]
        public float Cost { get; set; }
        [Required(ErrorMessage = Constants.Messages.RequiredField)]
        public string Content { get; set; }
        public bool IsActive { get; set; }
        public List<string> Tags { get; set; }
    }
}
