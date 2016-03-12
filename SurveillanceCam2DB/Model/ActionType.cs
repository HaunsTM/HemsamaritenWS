﻿namespace Tellstick.Model
{
    using Tellstick.Model.Interfaces;

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

        public Enums.ActionTypes Type { get; set; }

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
