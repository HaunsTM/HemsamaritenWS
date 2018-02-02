using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Model.Interfaces;
using Newtonsoft.Json;

namespace Core.Model
{
    public class MediaActionType : IMediaActionType
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        public Enums.MediaActionTypeOption ActionTypeOption { get; set; }

        #region Navigation properties

        [JsonIgnore]
        public virtual List<MediaAction> MediaActions { get; set; }

        #endregion

        public MediaActionType()
        {
            this.MediaActions = new List<MediaAction>();
        }
    }
}
