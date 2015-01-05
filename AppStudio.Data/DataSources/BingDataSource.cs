using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class BingDataSource : DataSourceBase<BingSchema>
    {
        private const string _url = @"http://www.bing.com/search?q=PewDiePie loc:ie&format=rss";

        protected override string CacheKey
        {
            get { return "BingDataSource"; }
        }

        public override bool HasStaticData
        {
            get { return false; }
        }

        public async override Task<IEnumerable<BingSchema>> LoadDataAsync()
        {
            try
            {
                var rssDataProvider = new RssDataProvider(_url);
                var syndicationItems = await rssDataProvider.Load();
                return from r in syndicationItems
                        select new BingSchema()
                        {
                            Title = r.Title,
                            Summary = r.Summary,
                            Link = r.FeedUrl,
                            Published = r.PublishDate
                        };
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("BingDataSourceDataSource.LoadData", ex.ToString());
                return new BingSchema[0];
            }
        }
    }
}
