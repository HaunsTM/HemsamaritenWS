﻿namespace Tellstick.BLL
{
    using Tellstick.BLL.Interfaces;
    using System.Linq;

    public class DatabaseDealer : IDatabaseDealer
    {
        public string DbConnectionStringName { get; private set; }

        public DatabaseDealer(string dbConnectionStringName)
        {
            this.DbConnectionStringName = dbConnectionStringName;
        }

        /// <summary>
        /// Creates and initializes a database
        /// </summary>
        /// <returns>True if the database were created successfully</returns>
        public bool CreateAndInitializeTellstickDB()
        {
            var databaseCreated = false;

            System.Data.Entity.Database.SetInitializer(new Tellstick.Model.DefaultDataDbInitializer());

            using (var db = new Tellstick.Model.TellstickDBContext(this.DbConnectionStringName))
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