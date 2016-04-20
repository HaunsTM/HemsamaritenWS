namespace WCF.BLL
{
    using System.Linq;

    using WCF.BLL.Interfaces;

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

            System.Data.Entity.Database.SetInitializer(new WCF.Model.DefaultDataDbInitializer());

            using (var db = new WCF.Model.HemsamaritenContext(dbConnectionStringName))
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