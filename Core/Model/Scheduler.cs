namespace Core.Model
{
    using Core.Model.Interfaces;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    public class Scheduler : IEntity, IScheduler
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
        public virtual List<TellstickAction> Actions { get; set; }

        #endregion

        public Scheduler()
        {
            this.Actions = new List<TellstickAction>();
        }

    }
}
