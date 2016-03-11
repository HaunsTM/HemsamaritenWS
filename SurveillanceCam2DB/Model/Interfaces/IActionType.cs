namespace SurveillanceCam2DB.Model.Interfaces
{
    public interface IActionType : IEntity
    {
        Enums.ActionTypes Type { get; set; }
    }
}