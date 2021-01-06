using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services.ShorteningURL.KeysGenerationService.DataAccess
{
    public class KGSDBService : IKGSDBService 
    {
        private readonly IDesignTimeDbContextFactory<KGSDbContext> _dbContextFactory;
        public KGSDBService(IDesignTimeDbContextFactory<KGSDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        private KGSDbContext GetContext() => _dbContextFactory.CreateDbContext(null);

       
        public async Task<int> InsertNewRound(Kgsround kgsround)
        {
            List<Kgsround> rounds;
            using (var context = GetContext())
            {
                var FromKey = new SqlParameter("FromKey", kgsround.FromKey );
                var ToKey = new SqlParameter("ToKey", kgsround.ToKey);
                var LastCounter = new SqlParameter("LastCounter", kgsround.LastCounter);
                var RoundDate = new SqlParameter("RoundDate", kgsround.RoundDate);
                rounds = await context.Kgsrounds.FromSqlRaw("execute dbo.InsertNewRound @FromKey,@ToKey,@LastCounter,@RoundDate", FromKey, ToKey, LastCounter, RoundDate).ToListAsync();
            }
            if (rounds != null && rounds.Count > 0)
            {
                return decimal.ToInt32(rounds.FirstOrDefault().RoundId);
            }
            throw new System.Exception("Round Data is not saved in DB");
        }

        public async Task<Kgsround> GetLatestKGSRound()
        {
            List<Kgsround> rounds;
            using (var context = GetContext())
            {               
                rounds = await context.Kgsrounds.FromSqlRaw("execute dbo.GetLatestKGSRound").ToListAsync();
            }
            if (rounds != null && rounds.Count > 0)
            {
                return rounds.FirstOrDefault();
            }
            return null;
        }
    }    
}
