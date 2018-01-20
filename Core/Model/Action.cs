using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Model.Interfaces;
using Newtonsoft.Json;

namespace Core.Model
{
    public abstract class Action : IEntity
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        #region Navigation properties

        [JsonIgnore]
        public virtual List<PerformedAction> PerformedActions { get; set; }

        [JsonIgnore]
        public virtual Scheduler Scheduler { get; set; }

        [ForeignKey("Scheduler")]
        public int? Scheduler_Id { get; set; }

        #endregion

        public Action()
        {
            this.PerformedActions = new List<PerformedAction>();
        }
    }
}
