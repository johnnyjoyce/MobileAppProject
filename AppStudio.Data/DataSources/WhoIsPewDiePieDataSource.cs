using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class WhoIsPewDiePieDataSource : DataSourceBase<WhoIsPewDiePieSchema>
    {
        private const string _file = "/Assets/Data/WhoIsPewDiePieDataSource.json";

        protected override string CacheKey
        {
            get { return "WhoIsPewDiePieDataSource"; }
        }

        public override bool HasStaticData
        {
            get { return true; }
        }

        public async override Task<IEnumerable<WhoIsPewDiePieSchema>> LoadDataAsync()
        {
            try
            {
                var serviceDataProvider = new StaticDataProvider(_file);
                return await serviceDataProvider.Load<WhoIsPewDiePieSchema>();
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("WhoIsPewDiePieDataSource.LoadData", ex.ToString());
                return new WhoIsPewDiePieSchema[0];
            }
        }
    }
}
