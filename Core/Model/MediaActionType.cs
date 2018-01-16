using System.ComponentModel.DataAnnotations;
using Core.Model.Interfaces;

namespace Core.Model
{
    public class MediaActionType : IMediaActionType
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

    }
}
