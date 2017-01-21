using System;

namespace Tellstick.Model.Interfaces.ViewModel
{
    public interface IUnitPerformedAction
    {
        int UnitId { get; set; }
        string Name { get; set; }
        string LocationDesciption { get; set; }

        long LatestActionTimeUTC { get; set; }

        string LatestSetActionType { get; set; }
    }
}
