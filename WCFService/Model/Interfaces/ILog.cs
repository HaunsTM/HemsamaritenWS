namespace WCFService.Model.Interfaces
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public interface ILog
    {
        [Key]
        int Id { get; set; }
        DateTime Date { get; set; }
        string Thread { get; set; }
        string Level { get; set; }
        string Logger { get; set; }
        string Message { get; set; }
        string Exception { get; set; }
    }
}