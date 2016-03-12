﻿namespace Tellstick.Model
{
    using Tellstick.Model.Interfaces;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    public class TellstickUnit : IEntity, ITellstickUnit
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        public string Name { get; set; }
        public string LocationDesciption { get; set; }

        #region Navigation properties

        [JsonIgnore]
        public virtual List<TellstickAction> TellstickActions { get; set; }

        public virtual TellstickParameter TellstickParameter { get; set; }
        public virtual TellstickProtocol TellstickProtocol { get; set; }
        public virtual TellstickModel TellstickModel { get; set; }

        #endregion

        public TellstickUnit()
        {
            this.TellstickActions = new List<TellstickAction>();
        }
    }
}
