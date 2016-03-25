namespace SurveillanceCam2DB.Model
{
    using SurveillanceCam2DB.Model.Interfaces;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Newtonsoft.Json;

    public class Action : IEntity, IAction
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion
        
        [ForeignKey("Camera")]
        public int Camera_Id { get; set; }
        [ForeignKey("ActionType")]
        public int ActionType_Id { get; set; }
        [ForeignKey("Scheduler")]
        public int Scheduler_Id { get; set; }

        #region Navigation properties

        [JsonIgnore]
        public virtual Camera Camera { get; set; }
        [JsonIgnore]
        public virtual ActionType ActionType { get; set; }
        [JsonIgnore]
        public virtual Scheduler Scheduler { get; set; }
        [JsonIgnore]
        public virtual List<PerformedAction> PerformedActions { get; set; }

        #endregion

        public Action()
        {
            this.PerformedActions = new List<PerformedAction>();
        }
    }
}
