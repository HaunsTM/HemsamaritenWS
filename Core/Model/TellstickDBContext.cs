namespace Core.Model
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

        public DbSet<TellstickAuthentication> Authentications { get; set; }
        public DbSet<TellstickZNetLiteV2> TellstickZNetLiteV2s { get; set; }
        public DbSet<TellstickActionType> ActionTypes { get; set; }

        public DbSet<TellstickUnit> Units { get; set; }
        public DbSet<TellstickAction> Actions { get; set; }
        public DbSet<Scheduler> Schedulers { get; set; }
        
        public DbSet<PerformedAction> PerformedActions { get; set; }

    }
}
