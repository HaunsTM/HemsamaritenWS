namespace WCF.BLL.Interfaces
{
    interface IDatabaseDealer
    {   
        /// <summary>
        /// Creates and initializes a database used for f ex log4net
        /// </summary>
        /// <returns>True if the database were created successfully</returns>
        bool CreateAndInitializeHemsamaritenDB();
    }
}
