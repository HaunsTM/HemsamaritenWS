namespace Core.Model.Interfaces
{
    public interface ICountry : IEntity
    {
        string Name { get; set; }
        string Code { get; set; }
        string ISOAlpha2 { get; set; }
        string ISOAlpha3 { get; set; }
    }
}