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


        /// <summary>
        /// What the cron expression means in simple terms.
        /// </summary>
        public string CronDescription { get; set; }
        /// <summary>
        /// <example>http://www.cronmaker.com/</example>
        /// </summary>
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
