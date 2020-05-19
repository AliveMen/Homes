namespace Homes.Models
{
    public class SearchCriteria
    {
        public string[] ObjectIds { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }

        public string Keyword { get; set; }
    }
}