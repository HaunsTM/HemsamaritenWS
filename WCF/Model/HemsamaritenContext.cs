namespace WCF.Model
{
    using System.Data.Entity;

    public class HemsamaritenContext : DbContext
    {
        /// <summary>
        /// The constructor for derived context.
        /// </summary>
        /// <param name="nameOrConnectionString">Either the database name or a connection string, example: "name=DefaultConnection"</param>
        public HemsamaritenContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public DbSet<Log> Logs { get; set; }
    }
}
