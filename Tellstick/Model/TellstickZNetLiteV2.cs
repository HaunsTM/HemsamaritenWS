namespace Tellstick.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;
    using Tellstick.Model.Interfaces;

    public class TellstickZNetLiteV2 : ITellstickZNetLiteV2
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        public string BaseIP { get; set; }

        #region Navigation properties

        [JsonIgnore]
        public virtual List<Authentication> Authentications { get; set; }
        [JsonIgnore]
        public virtual List<ActionType> ActionTypes { get; set; }

        #endregion

        public TellstickZNetLiteV2()
        {
            this.Authentications = new List<Authentication>();
            this.ActionTypes = new List<ActionType>();
        }

    }
}
