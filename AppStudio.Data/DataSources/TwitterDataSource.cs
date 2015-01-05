using System;
using System.Windows;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class TwitterDataSource : DataSourceBase<TwitterSchema>
    {
        private const long OAuthKey = 1849;
        private const string TwitterQuery = "Felix Kjellberg";

        protected override string CacheKey
        {
            get { return "TwitterDataSource"; }
        }

        public override bool HasStaticData
        {
            get { return false; }
        }

        public async override Task<IEnumerable<TwitterSchema>> LoadDataAsync()
        {
            try
            {
                var provider = new TwitterProvider();

                return await provider.SearchAsync(TwitterQuery, OAuthTokensRepository.GetTokens(OAuthKey));
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("TwitterDataSourceDataSource.LoadData", ex.ToString());
                return new TwitterSchema[0];
            }
        }
    }
}
