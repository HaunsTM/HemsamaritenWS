namespace Core.BLL
{
    using System.Linq;
    using log4net;
    using Core.BLL.Interfaces;
    using Core.Model;

    public class UnitDealer : IUnitDealer
    {

        private static readonly ILog log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string DbConnectionStringName { get; private set; }

        public UnitDealer(string dbConnectionStringName)
        {
            DbConnectionStringName = dbConnectionStringName;
        }

        public TellstickUnit UnitBy(int id)
        {
            using (var db = new Core.Model.HemsamaritenWindowsServiceDbContext(DbConnectionStringName))
            {
                var unit = from u in db.TellstickUnits
                            where u.Id == id
                            select u;

                return unit.FirstOrDefault();
            }
        }

        public TellstickUnit UnitByNativeDevice(int nativeDeviceId)
        {
            using (var db = new Core.Model.HemsamaritenWindowsServiceDbContext(DbConnectionStringName))
            {
                var unit = (from u in db.TellstickUnits
                            where u.NativeDeviceId == nativeDeviceId
                            select u).FirstOrDefault();

                return unit;
            }
        }
    }
}