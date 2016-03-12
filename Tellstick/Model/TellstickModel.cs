namespace Tellstick.Model
{
    using Tellstick.Model.Interfaces;

    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class TellstickModel : IEntity, ITellstickModel
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        public Enums.TellstickModel Name { get; set; }

        #region Navigation properties

        [JsonIgnore]
        public virtual List<TellstickUnit> TellstickUnits { get; set; }

        #endregion

        public TellstickModel()
        {
            this.TellstickUnits = new List<TellstickUnit>();
        }

    }
}
