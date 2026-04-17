using Database;
using Order.Services;
using Modify.Helpers;

namespace Modify.Services
{
    public class ModifyOrders
    {
        public static void Modify(Orders order)
        {
            using (OrderDBContext db = new OrderDBContext())
            {
                db.Orders.Update(order);
                db.SaveChanges();
            }
        }

        public static void Modify()
        {
            FormulationModify.Modify();
        }
    }
}
