using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Model.Interfaces;
using Newtonsoft.Json;

namespace Core.Model
{
    public class MediaSourceCategory : IMediaSourceCategory
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        public Enums.MediaCategoryType MediaCategoryType{ get; set; }

        #region Navigation properties
        
        [JsonIgnore]
        public virtual List<MediaSource> MediaSources { get; set; }

        #endregion

        public MediaSourceCategory()
        {
            this.MediaSources = new List<MediaSource>();
        }
    }
}
