using System;

namespace Tellstick.Model.ViewModel
{
    public interface ILastPerformedTellstickAction
    {
        DateTime Performed { get; set; }
        string NameOfPerfomee { get; set; }
        Enums.ActionTypeOption NameOfPerformedAction { get; set; }
    }
}