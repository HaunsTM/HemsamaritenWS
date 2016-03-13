namespace Tellstick.Model
{
    
    using System.Data.Entity;

    public class TellstickDBContext : DbContext
    {
        /// <summary>
        /// The constructor for derived context.
        /// </summary>
        /// <param name="nameOrConnectionString">Either the database name or a connection string, example: "name=DefaultConnection"</param>
        public TellstickDBContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public DbSet<TellstickProtocol> TellstickProtocols { get; set; }
        public DbSet<TellstickModel> TellstickModels { get; set; }
        public DbSet<TellstickActionType> TellstickActionTypes { get; set; }

        public DbSet<TellstickUnit> TellstickUnits { get; set; }
        public DbSet<TellstickAction> TellstickActions { get; set; }
        public DbSet<TellstickScheduler> TellstickSchedulers { get; set; }

        public DbSet<TellstickParameter> TellstickParameters { get; set; }
        public DbSet<PerformedAction> PerformedActions { get; set; }

    }
}
