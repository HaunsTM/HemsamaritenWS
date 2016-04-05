namespace Tellstick.Model
{
    using Tellstick.Model.Interfaces;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Newtonsoft.Json;

    public class Unit : IEntity, IUnit
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        [Index(IsUnique = true)]
        public int NativeDeviceId { get; set; }
        public string Name { get; set; }
        public string LocationDesciption { get; set; }

        #region Navigation properties

        [JsonIgnore]
        public virtual List<Action> Actions { get; set; }
        [JsonIgnore]
        public virtual Parameter Parameter { get; set; }
        [JsonIgnore]
        public virtual Protocol Protocol { get; set; }
        [JsonIgnore]
        public virtual ModelTypeAndTellstickModel ModelTypeAndTellstickModel { get; set; }

        [ForeignKey("Parameter")]
        public int Parameter_Id { get; set; }
        [ForeignKey("Protocol")]
        public int Protocol_Id { get; set; }
        [ForeignKey("Model")]
        public int Model_Id { get; set; }

        #endregion

        public Unit()
        {
            this.Actions = new List<Action>();
        }
    }
}
