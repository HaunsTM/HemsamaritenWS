namespace Tellstick.Model
{
    using Tellstick.Model.Interfaces;

    using System.ComponentModel.DataAnnotations;

    public class TellstickProtocol : IEntity, ITellstickProtocol
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        public Enums.TellstickProtocol Name { get; set; }

        #region Navigation properties

        #endregion

        public TellstickProtocol()
        {
        }

    }
}
