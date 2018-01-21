using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Model.Interfaces;
using Newtonsoft.Json;

namespace Core.Model
{
    public class MediaOutput : IMediaOutput
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        #region Navigation properties

        [JsonIgnore]
        public virtual List<MediaAction> MediaActions { get; set; }

        #endregion

        public MediaOutput()
        {
            this.MediaActions = new List<MediaAction>();
        }
    }
}