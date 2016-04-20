namespace WCFService.BLL
{
    using WCFService.BLL.Interfaces;

    using System.Linq;

    public class DatabaseDealer : IDatabaseDealer
    {
        public string DbConnectionStringName { get; private set; }

        public DatabaseDealer(string dbConnectionStringName)
        {
            this.DbConnectionStringName = dbConnectionStringName;
        }

        public bool CreateDB(string dbConnectionStringName)
        {
            var databaseCreated = false;

            System.Data.Entity.Database.SetInitializer(new WCFService.Model.DefaultDataDbInitializer());

            using (var db = new WCFService.Model.HemsamaritenContext(dbConnectionStringName))
            {
                db.Database.Initialize(true);

                //do something random stupid to force seed
                var stupidValue = db.Logs.Count();

                databaseCreated = true;
            }

            return databaseCreated;
        }
    }
}