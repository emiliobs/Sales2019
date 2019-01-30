namespace SalesBackend.Models
{
    using SalesDomain;

    public class LocalDataContext  : DataContext
    {
        public System.Data.Entity.DbSet<SalesCommon.Product> Products { get; set; }
    }
}