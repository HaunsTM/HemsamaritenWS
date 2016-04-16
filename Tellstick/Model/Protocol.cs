namespace Tellstick.Model
{
    using Tellstick.Model.Interfaces;

    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class Protocol : IEntity, IProtocol
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        public string Name { get { return this.Type.ToString(); } }

        public Tellstick.Model.Enums.ProtocolOption Type { get; set; }

        #region Navigation properties

        [JsonIgnore]
        public virtual List<Unit> Units { get; set; }

        #endregion

        public Protocol()
        {
            this.Units = new List<Unit>();
        }

    }
}
