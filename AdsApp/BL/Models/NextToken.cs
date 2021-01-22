namespace BL.Models
{
    public class NextToken
    {
        public int LastShownAdId { get; set; }
        public AdCategory? Category { get; set; }
        public AdType? Type { get; set; }
    }
}
