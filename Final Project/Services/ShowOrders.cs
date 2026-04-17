using Database;
using Order.Services;

namespace Show.Services
{
    public class ShowOrders
    {
        public static Orders? GetById(int id)
        {
            using (OrderDBContext db = new OrderDBContext())
            {
                return db.Orders.Find(id);
            }
        }

        public static int GetTotalCount()
        {
            using (OrderDBContext db = new OrderDBContext())
            {
                return db.Orders.Count();
            }
        }

        public static void ShowQuickList()
        {
            using (OrderDBContext db = new OrderDBContext())
            {
                int totalCount = db.Orders.Count();

                if (totalCount == 0)
                {
                    Console.WriteLine("No orders found.");
                    return;
                }

                Console.WriteLine($"\n--- Orders (Total: {totalCount}) ---");
                var orders = db.Orders.Take(20).ToList();
                foreach (var order in orders)
                {
                    Console.WriteLine($"ID: {order.id}, Name: {order.name} {order.lastName}, Plate: {order.plate}");
                }

                if (totalCount > 20)
                {
                    Console.WriteLine($"... and {totalCount - 20} more orders.");
                }
                Console.WriteLine("----------------------------------------");
            }
        }

        public static void Show()
        {
            using (OrderDBContext db = new OrderDBContext())
            {
                int pageSize = 20;
                int pageNumber = 0;
                int totalCount = db.Orders.Count();

                if (totalCount == 0)
                {
                    Console.WriteLine("No orders found.");
                    return;
                }

                while (true)
                {
                    var orders = db.Orders
                        .Skip(pageNumber * pageSize)
                        .Take(pageSize)
                        .ToList();

                    Console.WriteLine($"\n--- Orders (Page {pageNumber + 1} of {(totalCount + pageSize - 1) / pageSize}) ---");
                    foreach (var order in orders)
                    {
                        Console.WriteLine($"ID: {order.id}, Name: {order.name} {order.lastName}, Plate: {order.plate}");
                    }

                    if ((pageNumber + 1) * pageSize >= totalCount)
                    {
                        Console.WriteLine("\n[End of list. Press any key to continue...]");
                        Console.ReadLine();
                        break;
                    }

                    Console.WriteLine("\nPress Enter for next page, 'back' to return to menu...");
                    string? input = Console.ReadLine();
                    if (input?.Trim().ToLower() == "back")
                        break;

                    pageNumber++;
                }
            }
        }
    }
}
