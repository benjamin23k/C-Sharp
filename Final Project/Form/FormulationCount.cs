using Helpers;
using Count.Services;

namespace Count.Helpers
{
    public class FormulationCount
    {
        public static void Count()
        {
            ConsoleHelper.ShowHeader("Order Count");

            int count = CountOrders.GetCount();

            if (count == 0)
            {
                Console.WriteLine("There are no registered orders.");
            }
            else if (count == 1)
            {
                Console.WriteLine("There is 1 registered order.");
            }
            else
            {
                Console.WriteLine($"There are {count} registered orders.");
            }

            ConsoleHelper.Pause();
        }
    }
}
