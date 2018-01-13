using System;

namespace Tellstick.Model.ViewModel
{
    public interface ILastPerformedTellstickAction
    {
        DateTime Time { get; set; }
        string Name { get; set; }
        string PerformedActionDescription { get; set; }
    }
}