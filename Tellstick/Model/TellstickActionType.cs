﻿namespace Tellstick.Model
{
    using System.Collections.Generic;

    using Tellstick.Model.Interfaces;

    using System.ComponentModel.DataAnnotations;

    using Newtonsoft.Json;

    public class TellstickActionType : IEntity, ITellstickActionType
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion


        public Enums.TellstickActionType Action { get; set; }
        public int DimValue { get; set; }

        #region Navigation properties

        [JsonIgnore]
        public virtual List<TellstickAction> TellstickActions { get; set; }

        #endregion

        public TellstickActionType()
        {
            this.TellstickActions = new List<TellstickAction>();
        }
    }
}
