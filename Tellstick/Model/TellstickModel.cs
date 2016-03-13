namespace Tellstick.Model
{
    using System;

    using Tellstick.Model.Interfaces;

    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using System.ComponentModel;

    using Newtonsoft.Json;

    using Tellstick.Model.Enums;

    public class TellstickModel : IEntity, ITellstickModel
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        public string Model
        {
            get
            {
                var type = Type.GetAttributeOfType<DescriptionAttribute>().Description;
                var manufacturer = Manufacturer.GetAttributeOfType<DescriptionAttribute>().Description;
                var model = String.Format("{0}:{1}", type, manufacturer);
                return model;
            }
        }

        public Enums.EnumTellstickModelType Type { get; set; }
        public Enums.EnumTellstickModelManufacturer Manufacturer { get; set; }

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
