namespace API.Models
{
    public class CompanyInfo
    {
        public int Id { get; set; }
        public string Title { get; set; } = "¿Quiénes somos?";
        public string HtmlContent { get; set; } = string.Empty;
        public string? HeroImageUrl { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

