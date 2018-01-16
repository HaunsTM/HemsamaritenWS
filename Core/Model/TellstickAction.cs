using System.ComponentModel.DataAnnotations.Schema;
using Core.Model.Interfaces;
using Newtonsoft.Json;

namespace Core.Model
{
    public class TellstickAction : Action, ITellstickAction
    {
        #region Navigation properties
        
        [JsonIgnore]
        public virtual TellstickUnit TellstickUnit { get; set; }
        [JsonIgnore]
        public virtual TellstickActionType TellstickActionType { get; set; }

        [ForeignKey("TellstickUnit")]
        public int? TellstickUnit_Id { get; set; }
        [ForeignKey("TellstickActionType")]
        public int TellstickActionType_Id { get; set; }

        #endregion

        public TellstickAction()
        {
        }
    }
}
