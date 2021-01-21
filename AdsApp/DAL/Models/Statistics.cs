using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Statistics
    {
        public Dictionary<AdType,int> RequestsPerType { get; set; }
        public List<AdCategory> TopCategories { get; set; }
        public List<Ad> TopAds { get; set; }
    }
}
