namespace Tellstick.Model
{
    using Tellstick.Model.Interfaces;

    using System.ComponentModel.DataAnnotations;

    public class TellstickUnit : IEntity, ITellstickUnit
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        public string Name { get; set; }
        public string LocationDesciption { get; set; }

        #region Navigation properties

        #endregion

        public TellstickUnit()
        {
        }
    }
}
