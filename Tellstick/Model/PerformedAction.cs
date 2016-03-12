
namespace Tellstick.Model
{
    using Tellstick.Model.Interfaces;

    using System;
    using System.ComponentModel.DataAnnotations;

    public class PerformedAction : IEntity, IPerformedAction
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        public DateTime TimeOfPerformance { get; set; }

        #region Navigation properties

        #endregion

        public PerformedAction()
        {
        }

    }
}
