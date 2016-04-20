namespace WCFService.Model
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    public class DefaultDataDbInitializer : DropCreateDatabaseIfModelChanges<HemsamaritenContext>
    {

        private List<Log> LogItems()
        {
            var logItems = new List<Log>
            {
                new Log() { Date = DateTime.Today, Exception = "NO EXCEPTION", Level = "NO LEVEL", Logger = "NO LOGGER", Message = "NO MESSAGE"}
            };

            return logItems;
        }

        protected override void Seed(HemsamaritenContext context)
        {
            context.Logs.AddRange(this.LogItems());

            context.SaveChanges();
        }
    }
}
