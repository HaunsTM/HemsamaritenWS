namespace Tellstick.Model
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;
    using Tellstick.Model.Interfaces;

    public class Authentication : IAuthentication
    {

        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        public int Expires { get; set; }
        public string Token { get; set; }
        public DateTime Received { get; set; }

        #region Navigation properties

        [JsonIgnore]
        public virtual TellstickZNetLiteV2 TellstickZNetLiteV2 { get; set; }

        [ForeignKey("TellstickZNetLiteV2")]
        public int? TellstickZNetLiteV2_Id { get; set; }

        #endregion

        public Authentication()
        {

        }

    }
}