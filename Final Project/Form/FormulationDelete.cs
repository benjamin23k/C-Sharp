using Field.Helpers;
using Show.Services;
using Order.Services;
using Helpers;
using Delete.Services;

namespace Delete.Helpers
{
    public class FormulationDelete
    {
        public static void Delete()
        {
            while (true)
            {
                ShowOrders.ShowQuickList();
                var deleteResult = FieldInputHelper.ReadStringField(
                    "\nEnter The Name or ID To Delete (type 'back' to cancel): ",
                    null
                );

                if (deleteResult.Cancelled) return;
                if (string.IsNullOrWhiteSpace(deleteResult.Value)) continue;

                Orders? order = FindOrder(deleteResult.Value!);
                if (order == null)
                {
                    ConsoleHelper.ShowError("Order with the specified ID or Name not found.");
                    continue;
                }

                Console.WriteLine("\nOrder found:");
                OrderConfirmationHelper.ShowOrderDetails(order);

                var confirmResult = FieldInputHelper.ReadConfirm(
                    "Are you sure you want to delete this order? (Y/N): "
                );

                if (confirmResult.Cancelled || !confirmResult.Value)
                {
                    Console.WriteLine("Deletion cancelled.");
                    continue;
                }

                try
                {
                    DeleteOrders.Delete(order);
                    ConsoleHelper.ShowSuccess("Order deleted successfully!");
                }
                catch (Exception ex)
                {
                    ConsoleHelper.ShowError($"Failed to delete from database. Error: {ex.InnerException?.Message ?? ex.Message}");
                }

                var continueResult = FieldInputHelper.ReadConfirm("Do you want to delete another order? (Y/N): ");
                if (continueResult.Cancelled || !continueResult.Value)
                {
                    return;
                }
            }
        }

        private static Orders? FindOrder(string? searchValue)
        {
            if (string.IsNullOrWhiteSpace(searchValue)) return null;

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
