namespace Tellstick.Model
{
    using Tellstick.Model.Interfaces;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Newtonsoft.Json;

    public class PerformedAction : IEntity, IPerformedAction
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        /// <summary>
        /// Time of performance
        /// </summary>
        public DateTime Time { get; set; }

        #region Navigation properties

        [JsonIgnore]
        public virtual List<TellstickAction> TellstickActions { get; set; }

        #endregion

        public PerformedAction()
        {
            this.TellstickActions = new List<TellstickAction>();
        }

    }
}
