using System.Data.Entity;

namespace Core.Model
{
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

        public DbSet<TellstickAuthentication> TellstickAuthentications { get; set; }
        public DbSet<TellstickZNetLiteV2> TellstickZNetLiteV2s { get; set; }
        public DbSet<TellstickActionType> TellstickActionTypes { get; set; }
        public DbSet<TellstickUnit> TellstickUnits { get; set; }
        public DbSet<TellstickAction> TellstickActions { get; set; }

        public DbSet<MediaSource> MediaSources { get; set; }
        public DbSet<MediaOutput> MediaOutputs { get; set; }
        public DbSet<MediaOutputSetting> MediaOutputSettings { get; set; }
        public DbSet<MediaAction> MediaActions { get; set; }



        public DbSet<Scheduler> Schedulers { get; set; }
        
        public DbSet<PerformedAction> PerformedActions { get; set; }

    }
}
