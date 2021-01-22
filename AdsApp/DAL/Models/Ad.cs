using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public class Ad
    {
        public int Id { get; set; }
        public AdType Type { get; set; }
        public AdCategory Category { get; set; }
        public float Cost { get; set; }
        public string Content { get; set; }
        public List<Tag> Tags { get; set; }

        public int Views { get; set; }
        public bool IsActive { get; set; }
        public DateTime Updated { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
