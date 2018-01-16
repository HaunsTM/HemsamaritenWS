using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Model.Interfaces;
using Newtonsoft.Json;

namespace Core.Model
{

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
        public virtual List<TellstickAuthentication> Authentications { get; set; }
        [JsonIgnore]
        public virtual List<TellstickActionType> ActionTypes { get; set; }

        #endregion

        public TellstickZNetLiteV2()
        {
            this.Authentications = new List<TellstickAuthentication>();
            this.ActionTypes = new List<TellstickActionType>();
        }

    }
}
