namespace Tellstick.Model
{
    using Tellstick.Model.Interfaces;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Newtonsoft.Json;

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

        [JsonIgnore]
        public virtual List<TellstickAction> TellstickActions { get; set; }
        [JsonIgnore]
        public virtual TellstickParameter TellstickParameter { get; set; }
        [JsonIgnore]
        public virtual TellstickProtocol TellstickProtocol { get; set; }
        [JsonIgnore]
        public virtual TellstickModel TellstickModel { get; set; }

        [ForeignKey("TellstickParameter")]
        public int TellstickParameter_Id { get; set; }
        [ForeignKey("TellstickProtocol")]
        public int TellstickProtocol_Id { get; set; }
        [ForeignKey("TellstickModel")]
        public int TellstickModel_Id { get; set; }

        #endregion

        public TellstickUnit()
        {
            this.TellstickActions = new List<TellstickAction>();
        }
    }
}
