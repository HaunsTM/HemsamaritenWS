namespace Tellstick.Model.ViewModel
{
    public class LastPerformedTellstickAction : Tellstick.Model.ViewModel.ILastPerformedTellstickAction
    {
        public System.DateTime Performed { get; set; }
        public string NameOfPerfomee { get; set; }
        public Enums.ActionTypeOption  NameOfPerformedAction { get; set; }
    }   
}
