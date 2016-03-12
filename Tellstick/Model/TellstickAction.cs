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

        #endregion

        public TellstickAction()
        {
        }
    }
}
