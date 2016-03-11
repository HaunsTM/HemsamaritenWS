namespace SurveillanceCam2DB.Model
{
    using Interfaces;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Newtonsoft.Json;

    public class Position : IEntity, IPosition
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion
        
        public string Description { get; set; }

        #region Navigation properties

        [JsonIgnore]
        public virtual List<Image> Images { get; set; }

        #endregion

        public Position()
        {
            this.Images = new List<Image>();
        }
    }
}
