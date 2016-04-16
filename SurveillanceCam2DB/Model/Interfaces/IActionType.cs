namespace SurveillanceCam2DB.Model.Interfaces
{
    public interface IActionType : IEntity
    {
        SurveillanceCam2DB.Model.Enums.ActionTypes Name { get; set; }
    }
}