using Proyecto.UI.Models;
using System.Collections.Generic;

namespace Proyecto.UI.ViewModels
{
    public class AboutViewModel
    {
        public CompanyInfo? Company { get; set; }
        public List<TeamMember> Team { get; set; } = new();
    }
}

