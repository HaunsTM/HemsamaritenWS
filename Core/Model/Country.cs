using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Model.Interfaces;
using Newtonsoft.Json;

namespace Core.Model
{
    public class Country : ICountry
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        public string Name { get; set; }

        public string Code { get; set; }

        public string ISOAlpha2 { get; set; }

        public string ISOAlpha3 { get; set; }

        #region Navigation properties

        [JsonIgnore]
        public virtual List<MediaSource> MediaSources { get; set; }
        
        #endregion

        public Country()
        {
            this.MediaSources = new List<MediaSource>();
        }
    }
}
