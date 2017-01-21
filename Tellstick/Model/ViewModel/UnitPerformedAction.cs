namespace Tellstick.Model.ViewModel
{
    using System;
    using Tellstick.Model.Interfaces.ViewModel;

    public class UnitPerformedAction : IUnitPerformedAction
    {
        public UnitPerformedAction(int unitId, string name, string locationDesciption, long latestActionTimeUTC, string latestSetActionType)
        {
            UnitId = unitId;
            Name = name;
            LocationDesciption = locationDesciption;
            LatestActionTimeUTC = latestActionTimeUTC;
            LatestSetActionType = latestSetActionType;
        }

        public int UnitId { get; set; }
        public string Name { get; set; }
        public string LocationDesciption { get; set; }

        public long LatestActionTimeUTC { get; set; }
        public string LatestSetActionType { get; set; }
    }
}
