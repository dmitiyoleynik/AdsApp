using System.Collections.Generic;

namespace BL.Models
{
    public class Statistics
    {
        public Dictionary<AdType, int> RequestsPerType { get; set; }
        public List<AdCategory> TopCategories { get; set; }
        public List<Ad> TopAds { get; set; }
    }
}
