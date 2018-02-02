using System.Collections.Generic;
using System.Linq;
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

        public List<RegisteredMediaSource> PresetMediaSources()
        {
            //Which Unit are we talking about? Get Unit from DB
            using (var db = new Core.Model.HemsamaritenWindowsServiceDbContext(this.DbConnectionStringName))
            {
                var presetMediaSources = db.MediaSources.Where(mS => mS.Active).Select(mS => new RegisteredMediaSource
                {
                    MediaCategoryType = mS.MediaCategoryType,
                    Url = mS.Url,
                    Name = mS.Name
                }).ToList();
                return presetMediaSources;
            }
        }
    }
}
