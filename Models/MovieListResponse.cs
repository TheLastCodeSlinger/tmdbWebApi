namespace WebApplication1.Models
{
    public class MovieResponse
    {
        public Dates? Dates { get; set; }
        public int Page { get; set; }
        public List<SingleMovie> Results { get; set; } = new();
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
    }

    public class Dates
    {
        public string Maximum { get; set; } = "";
        public string Minimum { get; set; } = "";
    }
}