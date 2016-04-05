namespace Tellstick.Model
{
    using Tellstick.Model.Enums;
    using Tellstick.Model.Interfaces;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    public class Parameter : IEntity, IParameter
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        public Parameter_House House { get; set; }
        public Parameter_Unit Unit { get; set; }

        #region Navigation properties

        [JsonIgnore]
        public virtual List<Unit> Units { get; set; }

        #endregion

        public Parameter()
        {
            this.Units = new List<Unit>();
        }
    }
}
