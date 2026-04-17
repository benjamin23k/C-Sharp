using Show.Services;
using Field.Helpers;
using Search.Services;
using Helpers;
using Order.Services;

namespace Search.Helpers
{
    public class FormulationSearch
    {
        public static void Search()
        {
            ConsoleHelper.ShowHeader("Search Orders");

            while (true)
            {
                ShowOrders.ShowQuickList();
                var inputResult = FieldInputHelper.ReadStringField(
                    "\nEnter The Name or ID To Search (type 'back' to cancel): ",
                    null
                );

                if (inputResult.Cancelled) return;
                if (string.IsNullOrWhiteSpace(inputResult.Value))
                {
                    ConsoleHelper.ShowError("Invalid input");
                    continue;
                }

                Orders? order = FindSingleOrder(inputResult.Value);
                if (order != null)
                {
                    Console.WriteLine("\nOrder found:");
                    OrderConfirmationHelper.ShowOrderDetails(order);
                }
                else
                {
                    ConsoleHelper.ShowError("Order with the specified ID or Name not found.");
                }

                ConsoleHelper.Pause();
            }
        }

        private static Orders? FindSingleOrder(string searchValue)
        {
            using var db = new Database.OrderDBContext();
            bool isId = int.TryParse(searchValue, out int id);
            string lowerSearch = searchValue.ToLower();

            var matchingOrders = db.Orders.Where(o =>
                (isId && o.id == id) ||
                (o.name != null && o.name.ToLower().Contains(lowerSearch)) ||
                (o.lastName != null && o.lastName.ToLower().Contains(lowerSearch))
            ).ToList();

            if (!matchingOrders.Any()) return null;
            if (matchingOrders.Count == 1) return matchingOrders.First();

           
            Console.WriteLine("\nMultiple orders found:");
            foreach (var order in matchingOrders)
            {
                OrderConfirmationHelper.ShowOrderDetails(order);
            }

            var idResult = FieldInputHelper.ReadIdField("Please enter the specific ID: ");
            if (idResult.Cancelled) return null;

            return matchingOrders.FirstOrDefault(o => o.id == idResult.Value);
        }
    }
}
