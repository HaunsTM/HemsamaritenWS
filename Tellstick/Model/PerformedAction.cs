namespace Tellstick.Model
{
    using Tellstick.Model.Interfaces;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

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
        public virtual TellstickAction TellstickAction { get; set; }

        [ForeignKey("TellstickAction")]
        public int TellstickAction_Id { get; set; }

        #endregion

        public PerformedAction()
        {
        }

    }
}
