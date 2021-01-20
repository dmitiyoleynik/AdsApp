using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Ad
    {
        public int Id { get; set; }
        public AdType Type { get; set; }
        public Category Category { get; set; }
        public float Cost { get; set; }
        public string Content { get; set; }
        public bool IsActive { get; set; }
    }
}
