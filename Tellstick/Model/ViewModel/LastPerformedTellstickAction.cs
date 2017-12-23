namespace Tellstick.Model.ViewModel
{
    public class LastPerformedTellstickAction : Tellstick.Model.ViewModel.ILastPerformedTellstickAction
    {
        public System.DateTime Time { get; set; }
        public string Name{ get; set; }
        public string PerformedActionDescription { get; set; }
    }   
}