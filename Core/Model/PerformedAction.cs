using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Model.Interfaces;
using Newtonsoft.Json;

namespace Core.Model
{
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
        public virtual Action Action { get; set; }

        [ForeignKey("Action")]
        public int? Action_Id { get; set; }

        #endregion

        public PerformedAction()
        {
        }
    }
}
