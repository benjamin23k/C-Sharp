using Field.Helpers;
using Helpers;
using Show.Services;
using Order.Services;

namespace Show.Helpers
{
    public class FormulationShow
    {
        public static void Show()
        {
            ConsoleHelper.ShowHeader("Show Order Details");

            while (true)
            {
                var inputResult = FieldInputHelper.ReadStringField(
                    "Enter The Name Or ID To Show (type 'back' to cancel): ",
                    null
                );

                if (inputResult.Cancelled) return;
                if (string.IsNullOrWhiteSpace(inputResult.Value))
                {
                    ConsoleHelper.ShowError("Input cannot be empty.");
                    continue;
                }

                Orders? order = FindSingleOrder(inputResult.Value);
                if (order != null)
                {
                    Console.WriteLine("\nOrder Details:");
                    OrderConfirmationHelper.ShowOrderDetails(order);
                }
                else
                {
                    ConsoleHelper.ShowError("No orders found.");
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
