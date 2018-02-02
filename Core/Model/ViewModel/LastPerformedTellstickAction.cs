namespace Core.Model.ViewModel
{
    public class LastPerformedTellstickAction : ILastPerformedTellstickAction
    {
        public System.DateTime Time { get; set; }
        public string Name{ get; set; }
        public string PerformedActionDescription { get; set; }
    }   
}