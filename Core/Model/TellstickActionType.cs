using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Model.Interfaces;
using Newtonsoft.Json;

namespace Core.Model
{
    public class TellstickActionType : ITellstickActionType
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        public Enums.ActionTypeOption ActionTypeOption { get; set; }

        #region Navigation properties

        [ForeignKey("TellstickZNetLiteV2")]
        public int? TellstickZNetLiteV2_Id { get; set; }

        [JsonIgnore]
        public virtual TellstickZNetLiteV2 TellstickZNetLiteV2 { get; set; }
        [JsonIgnore]
        public virtual List<TellstickAction> Actions { get; set; }

        #endregion

        public TellstickActionType()
        {
            this.Actions = new List<TellstickAction>();
        }
    }
}
