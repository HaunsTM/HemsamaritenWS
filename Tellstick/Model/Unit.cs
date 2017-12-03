namespace Tellstick.Model
{
    using Tellstick.Model.Interfaces;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    using Newtonsoft.Json;

    [DataContract]
    public class Unit : IEntity, IUnit
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
        public virtual List<Action> Actions { get; set; }

        #endregion

        public Unit()
        {
            this.Actions = new List<Action>();
        }
    }
}
