namespace Tellstick.Model
{
    using Tellstick.Model.Interfaces;

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Newtonsoft.Json;

    public class TellstickAction : IEntity, ITellstickAction
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        #region Navigation properties

        [JsonIgnore]
        public virtual PerformedAction PerformedAction { get; set; }
        [JsonIgnore]
        public virtual TellstickUnit TellstickUnit { get; set; }
        [JsonIgnore]
        public virtual TellstickActionType TellstickActionType { get; set; }
        [JsonIgnore]
        public virtual TellstickScheduler TellstickScheduler { get; set; }

        [ForeignKey("PerformedAction")]
        public int PerformedAction_Id { get; set; }
        [ForeignKey("TellstickUnit")]
        public int TellstickUnit_Id { get; set; }
        [ForeignKey("TellstickActionType")]
        public int TellstickActionType_Id { get; set; }
        [ForeignKey("TellstickScheduler")]
        public int TellstickScheduler_Id { get; set; }

        #endregion

        public TellstickAction()
        {
        }
    }
}
