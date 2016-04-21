namespace SurveillanceCam2DB.BLL
{
    using SurveillanceCam2DB.BLL.Interfaces;

    using System.Linq;

    public class DatabaseDealer : IDatabaseDealer
    {
        public string DbConnectionStringName { get; private set; }

        public DatabaseDealer(string dbConnectionStringName)
        {
            this.DbConnectionStringName = dbConnectionStringName;
        }

        /// <summary>
        /// Creates and initializes a database used for f ex log4net
        /// </summary>
        /// <returns>True if the database were created successfully</returns>
        public bool CreateAndInitializeSurveillanceCam2DBDB()
        {
            var databaseCreated = false;

            System.Data.Entity.Database.SetInitializer(new SurveillanceCam2DB.Model.DefaultDataDbInitializer());

            using (var db = new SurveillanceCam2DB.Model.SurveillanceCam2DBContext(this.DbConnectionStringName))
            {
                db.Database.Initialize(true);

                //do something random stupid to force seed
                var stupidValue = db.Schedulers.Count();

                databaseCreated = true;
            }

            return databaseCreated;
        }
    }
}