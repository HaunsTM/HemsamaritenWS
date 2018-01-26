using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Model.Interfaces;
using Newtonsoft.Json;

namespace Core.Model
{
    public class MediaOutputVolume : IMediaOutputVolume
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        [Range(0, 100, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Value { get; set; }
        public Enums.MediaOutputVolumeValue Label { get; set; }

        #region Navigation properties

        [JsonIgnore]
        public virtual List<MediaAction> MediaActions { get; set; }

        #endregion

        public MediaOutputVolume ()
        {
            this.MediaActions = new List<MediaAction>();
        }
    }
}
