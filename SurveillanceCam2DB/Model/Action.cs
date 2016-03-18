namespace Tellstick.Model
{
    using Tellstick.Model.Interfaces;

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Newtonsoft.Json;

    public class Action : IEntity, IAction
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        public string CronExpression { get; set; }

        #region Navigation properties

        [JsonIgnore]
        public virtual Camera Camera { get; set; }
        [JsonIgnore]
        public virtual ActionType ActionType { get; set; }

        [ForeignKey("Camera")]
        public int Camera_Id { get; set; }
        [ForeignKey("ActionType")]
        public int ActionType_Id { get; set; }

        #endregion

        public Action()
        {
        }
    }
}
