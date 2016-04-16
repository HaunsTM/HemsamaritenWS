namespace Tellstick.Model.Interfaces
{
    public interface IParameter : IEntity
    {
        Tellstick.Model.Enums.Parameter_HouseOption HouseOption { get; set; }
        Tellstick.Model.Enums.Parameter_UnitOption UnitOption { get; set; }
    }
}