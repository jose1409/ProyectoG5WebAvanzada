namespace Proyecto.UI.Models
{
    public class CompanyInfo
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string HtmlContent { get; set; } = string.Empty;
        public string? HeroImageUrl { get; set; }
        public bool IsActive { get; set; }
    }
}

