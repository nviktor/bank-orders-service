using LinqToDB.DataProvider.PostgreSQL;
using Microsoft.Extensions.Configuration;

namespace Bank.Digital.OrderService.DAL.Linq2Db.Context
{
    internal partial class OrderServiceDbContext
    {
        public OrderServiceDbContext(IConfiguration config)
            : base(new PostgreSQLDataProvider(PostgreSQLVersion.v95), config.GetConnectionString("OrderServiceDb"))
        {
        }
    }
}
