using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Model
{
    using Core.Model.Interfaces;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    public class ActionType : IEntity, IActionType
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
        public virtual List<Action> Actions { get; set; }

        #endregion

        public ActionType()
        {
            this.Actions = new List<Action>();
        }
    }
}
