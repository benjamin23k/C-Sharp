using Order.Services;
using Field.Helpers;

namespace Helpers
{
    public static class OrderConfirmationHelper
    {
        public static bool ConfirmOrder(Orders order)
        {
            Console.WriteLine("\n--- Summary ---");
            Console.WriteLine($"Name: {order.name}");
            Console.WriteLine($"Last: {order.lastName}");
            Console.WriteLine($"Plate: {order.plate}");
            Console.WriteLine("-------------------");

            var confirmResult = FieldInputHelper.ReadConfirm(
                "Are you sure you want to proceed? (Y/N): "
            );

            if (confirmResult.Cancelled || !confirmResult.Value)
            {
                Console.WriteLine("Operation cancelled.");
                return false;
            }

            return true;
        }

        public static void ShowOrderDetails(Orders order)
        {
            Console.WriteLine($"ID: {order.id}");
            Console.WriteLine($"Name: {order.name} {order.lastName}");
            Console.WriteLine($"Plate: {order.plate}");
            Console.WriteLine($"Registered: {order.createdAt:yyyy-MM-dd HH:mm}");
            Console.WriteLine($"Last Modified: {order.updatedAt:yyyy-MM-dd HH:mm}");
            Console.WriteLine("-------------------");
        }
    }
}
