namespace OS.API.Controllers.Oversite
{
    public class OversiteModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Website { get; set; }
        public string Category { get; set; }
        public string Severity { get; set; }
    }
}