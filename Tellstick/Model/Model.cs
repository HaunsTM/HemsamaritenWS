namespace Tellstick.Model
{
    using System;

    using Tellstick.Model.Interfaces;

    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using System.ComponentModel;

    using Newtonsoft.Json;

    using Tellstick.Model.Enums;

    public class Model : IEntity, IModel
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        public string Description
        {
            get
            {
                var type = this.TypeOption.GetAttributeOfType<DescriptionAttribute>().Description;
                var manufacturer = this.ManufacturerOption.GetAttributeOfType<DescriptionAttribute>().Description;
                var model = String.Format("{0}:{1}", type, manufacturer);
                return model;
            }
        }

        public Enums.ModelTypeOption TypeOption { get; set; }
        public Enums.ModelManufacturerOption ManufacturerOption { get; set; }

        #region Navigation properties

        [JsonIgnore]
        public virtual List<Unit> Units { get; set; }

        #endregion

        public Model()
        {
            this.Units = new List<Unit>();
            this.Units = new List<Unit>();
        }

    }
}
