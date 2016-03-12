namespace Tellstick.Model
{
    using Tellstick.Model.Interfaces;

    using System.ComponentModel.DataAnnotations;

    public class TellstickModel : IEntity, ITellstickModel
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        public Enums.TellstickModel Name { get; set; }

        #region Navigation properties

        #endregion

        public TellstickModel()
        {
        }

    }
}
