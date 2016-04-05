namespace Tellstick.Model.Interfaces
{
    public interface IParameter : IEntity
    {
        Model.Enums.Parameter_House House { get; set; }
        Model.Enums.Parameter_Unit Unit { get; set; }
    }
}