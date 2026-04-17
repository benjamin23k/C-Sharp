using System;
using Order.Services;
using Field.Helpers;
using Validators.Helpers;
using Database;
using Helpers;

namespace FormulationAdd.Helpers
{
    public class Formulation
    {
        public static void AddOrders()
        {
            while (true)
            {
                Orders p = new Orders();
                ConsoleHelper.ShowHeader("Add Order");
                Console.WriteLine("Type 'back' in any field to cancel.\n");

           
                string? name = LettersValidator.ReadOnlyLetters("Name: ", 1, 50);
                if (name == null)
                {
                    Console.WriteLine("Addition cancelled.");
                    ConsoleHelper.Pause();
                    return;
                }
                p.name = name;

                string? lastName = LettersValidator.ReadOnlyLetters("Last Name: ", 1, 50);
                if (lastName == null)
                {
                    Console.WriteLine("Addition cancelled.");
                    ConsoleHelper.Pause();
                    return;
                }
                p.lastName = lastName;

                string? plate = ReadUniquePlate();
                if (plate == null)
                {
                    Console.WriteLine("Addition cancelled.");
                    ConsoleHelper.Pause();
                    return;
                }
                p.plate = plate;


                if (!OrderConfirmationHelper.ConfirmOrder(p))
                {
                    ConsoleHelper.Pause();
                    continue;
                }

                using (OrderDBContext db = new OrderDBContext())
                {
                    try
                    {
                        db.Orders.Add(p);
                        db.SaveChanges();
                        ConsoleHelper.ShowSuccess("Order added successfully!");
                    }
                    catch (Exception ex)
                    {
                        ConsoleHelper.ShowError($"Failed to save to database. Error: {ex.InnerException?.Message ?? ex.Message}");
                    }
                }

                var continueResult = FieldInputHelper.ReadConfirm("Do you want to add another order? (Y/N): ");
                if (continueResult.Cancelled || !continueResult.Value)
                {
                    return;
                }
            }
        }

        private static string? ReadUniquePlate()
        {
            while (true)
            {
                var plateResult = FieldInputHelper.ReadField<string>(
                    "Plate: ",
                    input =>
                    {
                        if (string.IsNullOrWhiteSpace(input))
                            return (false, "", "The field cannot be empty.");

                        if (input.Length < 1 || input.Length > 25)
                            return (false, "", "Length must be between 1 and 25.");

                        if (!input.All(c => char.IsLetter(c) || c == ' ' || c == '-'))
                            return (false, "", "Only Letters, Spaces and Hyphens Are Allowed.");

                        if (!UniqueChecker.IsPlateUnique(input))
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
