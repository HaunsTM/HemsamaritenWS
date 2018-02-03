using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Model.Interfaces;
using Newtonsoft.Json;

namespace Core.Model
{
    public class MediaSource : IMediaSource
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        public string Name { get; set; }
        public string Url { get; set; }
        public string MediaDataBase64 { get; set; }

        public Core.Model.Enums.MediaCategoryType MediaCategoryType { get; set; }

        #region Navigation properties

        [JsonIgnore]
        public virtual List<MediaAction> MediaActions { get; set; }

        [JsonIgnore]
        public virtual Country MediaCountry { get; set; }

        [ForeignKey("Country")]
        public int? Country_Id { get; set; }

        #endregion

        public MediaSource()
        {
            this.MediaActions = new List<MediaAction>();
        }
    }
}
