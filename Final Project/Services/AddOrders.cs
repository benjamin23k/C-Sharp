using Database;
using Order.Services;
using FormulationAdd.Helpers;

namespace Add.Services
{
    public class AddOrders
    {
        public static void Add(Orders order)
        {
            using (OrderDBContext db = new OrderDBContext())
            {
                db.Orders.Add(order);
                db.SaveChanges();
            }
        }

        public static void Add()
        {
            Formulation.AddOrders();
        }
    }
}
