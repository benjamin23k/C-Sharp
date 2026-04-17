using Field.Helpers;
using Show.Services;
using Validators.Helpers;
using Helpers;
using Modify.Services;
using Order.Services;

namespace Modify.Helpers
{
    public class FormulationModify
    {
        public static void Modify()
        {
            while (true)
            {
                ShowOrders.ShowQuickList();
                var modifyResult = FieldInputHelper.ReadStringField(
                    "\nEnter The Name or ID To Modify (type 'back' to cancel): ",
                    null
                );

                if (modifyResult.Cancelled) return;
                if (string.IsNullOrWhiteSpace(modifyResult.Value)) continue;

                Orders? order = FindOrder(modifyResult.Value!);
                if (order == null)
                {
                    ConsoleHelper.ShowError("Order with the specified ID or Name not found.");
                    continue;
                }


                Console.WriteLine("\nOrder found:");
                OrderConfirmationHelper.ShowOrderDetails(order);


                var confirmResult = FieldInputHelper.ReadConfirm(
                    $"Are you sure you want to modify this order? (Y/N): "
                );

                if (confirmResult.Cancelled || !confirmResult.Value)
                {
                    Console.WriteLine("Modification cancelled.");
                    continue;
                }


                string? newName = LettersValidator.ReadOnlyLetters(
                    "Enter New Name (type 'back' to cancel): ",
                    1,
                    50
                );
                if (newName == null)
                {
                    Console.WriteLine("Modification cancelled.");
                    continue;
                }
                order.name = newName;

                string? newLastName = LettersValidator.ReadOnlyLetters(
                    "Enter New Last Name (type 'back' to cancel): ",
                    1,
                    50
                );
                if (newLastName == null)
                {
                    Console.WriteLine("Modification cancelled.");
                    continue;
                }
                order.lastName = newLastName;

                string? newPlate = ReadUniquePlateForModify(order.id);
                if (newPlate == null)
                {
                    Console.WriteLine("Modification cancelled.");
                    continue;
                }
                order.plate = newPlate;


                try
                {
                    ModifyOrders.Modify(order);
                    ConsoleHelper.ShowSuccess("Order modified successfully!");
                }
                catch (Exception ex)
                {
                    ConsoleHelper.ShowError($"Failed to modify database. Error: {ex.InnerException?.Message ?? ex.Message}");
                }

                var continueResult = FieldInputHelper.ReadConfirm("Do you want to modify another order? (Y/N): ");
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

        private static string? ReadUniquePlateForModify(int excludeId)
        {
            while (true)
            {
                var plateResult = FieldInputHelper.ReadField<string>(
                    "Enter new Plate (type 'back' to cancel): ",
                    input =>
                    {
                        if (string.IsNullOrWhiteSpace(input))
                            return (false, "", "The field cannot be empty.");

                        if (input.Length < 1 || input.Length > 25)
                            return (false, "", "Length must be between 1 and 25.");

                        if (!input.All(c => char.IsLetter(c) || c == ' ' || c == '-'))
                            return (false, "", "Only Letters, Spaces and Hyphens Are Allowed.");

                        if (!UniqueChecker.IsPlateUnique(input, excludeId))
                            return (false, "", "This plate already exists. Please enter a unique one.");

                        return (true, input, "");
                    }
                );

                if (plateResult.Cancelled) return null;
                return plateResult.Value;
            }
        }
    }
}
