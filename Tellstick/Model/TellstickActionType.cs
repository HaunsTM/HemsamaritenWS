namespace Tellstick.Model
{
    using Tellstick.Model.Interfaces;

    using System.ComponentModel.DataAnnotations;

    public class TellstickActionType : IEntity, ITellstickActionType
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion


        public Enums.TellstickActionType Action { get; set; }
        public int DimValue { get; set; }

        #region Navigation properties

        #endregion

        public TellstickActionType()
        {
        }
    }
}
