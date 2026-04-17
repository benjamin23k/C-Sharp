using Database;
using Order.Services;
using Search.Helpers;

namespace Search.Services
{
    public class SearchOrders
    {
        public static Orders? FindById(int id)
        {
            using (OrderDBContext db = new OrderDBContext())
            {
                return db.Orders.Find(id);
            }
        }

        public static Orders? FindByName(string name)
        {
            using (OrderDBContext db = new OrderDBContext())
            {
                return db.Orders.FirstOrDefault(o =>
                    o.name != null && o.name.ToLower() == name.ToLower());
            }
        }

        public static Orders? FindByLastName(string lastName)
        {
            using (OrderDBContext db = new OrderDBContext())
            {
                return db.Orders.FirstOrDefault(o =>
                    o.lastName != null && o.lastName.ToLower() == lastName.ToLower());
            }
        }

        public static void Search()
        {
            FormulationSearch.Search();
        }
    }
}
