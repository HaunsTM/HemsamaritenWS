using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Model.Interfaces;
using Newtonsoft.Json;

namespace Core.Model
{
    [Table("TellstickActions")]
    public class TellstickAction : Action, ITellstickAction
    {

        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

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
