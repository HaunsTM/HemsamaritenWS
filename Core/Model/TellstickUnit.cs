namespace Core.Model
{
    using Core.Model.Interfaces;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    using Newtonsoft.Json;

    [DataContract]
    public class TellstickUnit : IEntity, ITellstickUnit
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        [Index(IsUnique = true)]
        [DataMember]
        public int NativeDeviceId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string LocationDesciption { get; set; }

        #region Navigation properties

        [JsonIgnore]
        public virtual List<TellstickAction> Actions { get; set; }

        #endregion

        public TellstickUnit()
        {
            this.Actions = new List<TellstickAction>();
        }
    }
}
