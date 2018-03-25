using System.Collections.Generic;
using System.Linq;
using Core.Model;
using Core.Model.Interfaces;
using Core.Model.ViewModel;

namespace Core.BLL
{
    public class MediaSourceDealer
    {
        public string DbConnectionStringName { get; private set; }

        public MediaSourceDealer(string dbConnectionStringName)
        {
            DbConnectionStringName = dbConnectionStringName;
        }

        public List<RegisteredMediaSource> PredefinedMediaSourcesList()
        {
            //Which Unit are we talking about? Get Unit from DB
            using (var db = new Core.Model.HemsamaritenWindowsServiceDbContext(this.DbConnectionStringName))
            {
                var presetMediaSources = db.MediaSources.Where(mS => mS.Active).Select(mS => new RegisteredMediaSource
                {
                    MediaCategoryType = mS.MediaCategoryType.ToString(),
                    Url = mS.Url,
                    Name = mS.Name,
                    MediaSourceCountry = mS.MediaCountry.Name.ToString()
                }).ToList<RegisteredMediaSource>();
                return presetMediaSources;
            }
        }

        public List<RegisteredMediaSource> PredefinedMediaSourcesListBy(ICountry country)
        {
            //Which Unit are we talking about? Get Unit from DB
            using (var db = new Core.Model.HemsamaritenWindowsServiceDbContext(this.DbConnectionStringName))
            {
                var presetMediaSources = db.MediaSources
                    .Where(mS => mS.Active)
                    .Where(mS => mS.MediaCountry == country)
                    .Select(mS => new RegisteredMediaSource
                        {
                            MediaCategoryType = mS.MediaCategoryType.ToString(),
                            Url = mS.Url,
                            Name = mS.Name
                        }).ToList<RegisteredMediaSource>();
                return presetMediaSources;
            }
        }
    }
}
