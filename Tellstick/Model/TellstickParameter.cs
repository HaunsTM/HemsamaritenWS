namespace Tellstick.Model
{
    using Tellstick.Model.Enums;
    using Tellstick.Model.Interfaces;

    using System.ComponentModel.DataAnnotations;

    public class TellstickParameter : IEntity, ITellstickParameter
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        public TellstickParameter_House House { get; set; }
        public TellstickParameter_Unit Unit { get; set; }

        #region Navigation properties

        #endregion

        public TellstickParameter()
        {
        }
    }
}
