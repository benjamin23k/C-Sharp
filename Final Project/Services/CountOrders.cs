using Database;
using Count.Helpers;

namespace Count.Services
{
    public class CountOrders
    {
        public static int GetCount()
        {
            using (OrderDBContext db = new OrderDBContext())
            {
                return db.Orders.Count();
            }
        }

        public static void Count()
        {
            FormulationCount.Count();
        }
    }
}
