namespace Tellstick.Model
{
    using System;

    using Tellstick.Model.Interfaces;

    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using System.ComponentModel;

    using Newtonsoft.Json;

    using Tellstick.Model.Enums;

    public class ModelTypeAndTellstickModel : IEntity, IModelTypeAndTellstickModel
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

        public Enums.ModelType Type { get; set; }
        public Enums.ModelManufacturer Manufacturer { get; set; }

        #region Navigation properties

        [JsonIgnore]
        public virtual List<Unit> Units { get; set; }

        #endregion

        public ModelTypeAndTellstickModel()
        {
            this.Units = new List<Unit>();
        }

    }
}
