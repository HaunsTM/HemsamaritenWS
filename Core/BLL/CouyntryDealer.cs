using System.Collections.Generic;
using System.Linq;
using Core.Model.Interfaces;

namespace Core.BLL
{
    public class CouyntryDealer
    {
        public string DbConnectionStringName { get; private set; }

        public CouyntryDealer(string dbConnectionStringName)
        {
            DbConnectionStringName = dbConnectionStringName;
        }

        public List<ICountry> AllCountriesList()
        {
            using (var db = new Core.Model.HemsamaritenWindowsServiceDbContext(this.DbConnectionStringName))
            {
                var allCountries = db.Countries
                    .Where(c => c.Active)
                    .Select(c => c).ToList<ICountry>();
                return allCountries;
            }
        }

        public List<ICountry> CountriesRepresentedInMediaSourcesList()
        {
            using (var db = new Core.Model.HemsamaritenWindowsServiceDbContext(this.DbConnectionStringName))
            {
                var distinctCountriesRepresentedInMediaSourcesListIncludingNulls = db.MediaSources
                    .Where(c => c.Active)
                    .Where(c => c.MediaCountry != null)
                    .GroupBy(c => c.MediaCountry).Select(group => group.FirstOrDefault()).ToList<Core.Model.MediaSource>();
                var distinctCountries =
                    distinctCountriesRepresentedInMediaSourcesListIncludingNulls.Select(c => c.MediaCountry).ToList<ICountry>();
                return distinctCountries;
            }
        }
    }
}