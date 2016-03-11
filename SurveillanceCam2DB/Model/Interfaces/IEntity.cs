namespace SurveillanceCam2DB.Model.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }

        bool Active { get; set; }
    }
}