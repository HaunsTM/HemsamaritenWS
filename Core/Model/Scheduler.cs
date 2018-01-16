using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Model.Interfaces;
using Newtonsoft.Json;

namespace Core.Model
{
    public class Scheduler : IScheduler
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
