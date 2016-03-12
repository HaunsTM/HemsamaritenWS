namespace Tellstick.Model
{
    using Tellstick.Model.Interfaces;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    public class TellstickScheduler : IEntity, ITellstickScheduler
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        public string CronExpression { get; set; }

        #region Navigation properties
        
        [JsonIgnore]
        public virtual List<TellstickAction> TellstickActions { get; set; }

        #endregion

        public TellstickScheduler()
        {
            this.TellstickActions = new List<TellstickAction>();
        }
    }
}
