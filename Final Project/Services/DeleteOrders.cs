using Database;
using Order.Services;
using Delete.Helpers;

namespace Delete.Services
{
    public class DeleteOrders
    {
        public static void Delete(Orders order)
        {
            using (OrderDBContext db = new OrderDBContext())
            {
                db.Orders.Remove(order);
                db.SaveChanges();
            }
        }

        public static void Delete()
        {
            FormulationDelete.Delete();
        }
    }
}
