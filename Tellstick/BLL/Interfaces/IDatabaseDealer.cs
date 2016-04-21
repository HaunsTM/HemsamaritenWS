namespace Tellstick.BLL.Interfaces
{
    interface IDatabaseDealer
    {   
        /// <summary>
        /// Creates and initializes a database
        /// </summary>
        /// <returns>True if the database were created successfully</returns>
        bool CreateAndInitializeTellstickDB();
    }
}
