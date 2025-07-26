using API.Models;

namespace API.Repository
{
    public interface IAboutRepository
    {
        Task<CompanyInfo?> GetCompanyInfoAsync();
        Task<IEnumerable<TeamMember>> GetTeamMembersAsync();
    }
}

