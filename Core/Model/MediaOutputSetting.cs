using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Model.Interfaces;
using Newtonsoft.Json;

namespace Core.Model
{
    public class MediaOutputSetting : IMediaOutputSetting
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        #region Navigation properties

        [JsonIgnore]
        public virtual List<MediaActionType> MediaActionTypes { get; set; }

        #endregion

        public MediaOutputSetting()
        {
            this.MediaActionTypes = new List<MediaActionType>();
        }

    }
}
