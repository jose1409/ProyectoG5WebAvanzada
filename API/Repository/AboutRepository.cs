using System.Data;
using Dapper;
using API.Models;

namespace API.Repository
{
    public class AboutRepository : IAboutRepository
    {
        private readonly IDbConnection _db;

        public AboutRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<CompanyInfo?> GetCompanyInfoAsync()
        {
            var result = await _db.QueryFirstOrDefaultAsync<CompanyInfo>("sp_GetCompanyInfo", commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<IEnumerable<TeamMember>> GetTeamMembersAsync()
        {
            var result = await _db.QueryAsync<TeamMember>("sp_GetTeamMembers", commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}

