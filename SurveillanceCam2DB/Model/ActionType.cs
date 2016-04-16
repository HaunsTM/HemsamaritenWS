namespace SurveillanceCam2DB.Model
{
    using SurveillanceCam2DB.Model.Interfaces;

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

        public SurveillanceCam2DB.Model.Enums.ActionTypes Name { get; set; }

        #region Navigation properties

        [JsonIgnore]
        public virtual List<Action> Actions { get; set; }

        #endregion

        public ActionType()
        {
            this.Actions = new List<Action>();
        }
    }
}
