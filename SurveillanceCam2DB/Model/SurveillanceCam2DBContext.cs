namespace SurveillanceCam2DB.Model
{
    using System.Data.Entity;

    public class SurveillanceCam2DBContext : DbContext
    {
        /// <summary>
        /// The constructor for derived context.
        /// </summary>
        /// <param name="nameOrConnectionString">Either the database name or a connection string, example: "name=DefaultConnection"</param>
        public SurveillanceCam2DBContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public DbSet<Camera> Cameras { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<ActionType> ActionTypes { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ImageData> ImageData { get; set; }
        public DbSet<Position> Positions { get; set; }

    }
}
