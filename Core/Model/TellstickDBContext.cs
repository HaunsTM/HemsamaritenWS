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

        public DbSet<Authentication> Authentications { get; set; }
        public DbSet<TellstickZNetLiteV2> TellstickZNetLiteV2s { get; set; }
        public DbSet<ActionType> ActionTypes { get; set; }

        public DbSet<Unit> Units { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<Scheduler> Schedulers { get; set; }
        
        public DbSet<PerformedAction> PerformedActions { get; set; }

    }
}
