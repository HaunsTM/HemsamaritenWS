namespace Tellstick.Model
{
    using Tellstick.Model.Interfaces;

    using System.ComponentModel.DataAnnotations;

    public class TellstickAction : IEntity, ITellstickAction
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        #region Navigation properties


        public virtual PerformedAction PerformedAction { get; set; }
        public virtual TellstickUnit TellstickUnit { get; set; }
        public virtual TellstickActionType TellstickActionType { get; set; }
        public virtual TellstickScheduler TellstickScheduler { get; set; }

        #endregion

        public TellstickAction()
        {
        }
    }
}
