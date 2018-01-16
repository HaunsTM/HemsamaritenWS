namespace Core.Model
{
    using System.Collections.Generic;

    using Core.Model.Interfaces;

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
        public virtual List<PerformedAction> PerformedActions { get; set; }

        [JsonIgnore]
        public virtual TellstickUnit Unit { get; set; }
        [JsonIgnore]
        public virtual TellstickActionType ActionType { get; set; }
        [JsonIgnore]
        public virtual Scheduler Scheduler { get; set; }
        
        [ForeignKey("Unit")]
        public int? Unit_Id { get; set; }
        [ForeignKey("ActionType")]
        public int ActionType_Id { get; set; }
        [ForeignKey("Scheduler")]
        public int? Scheduler_Id { get; set; }

        #endregion

        public TellstickAction()
        {
            this.PerformedActions = new List<PerformedAction>();
        }
    }
}
