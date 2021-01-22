using System;

namespace DAL.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public DateTime? Deleted { get; set; }
    }
}