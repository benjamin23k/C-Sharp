namespace Helpers
{
    public static class ConsoleHelper
    {
        public static void ClearConsole()
        {
            Console.Clear();
        }

        public static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        public static void ShowHeader(string title)
        {
            Console.Clear();
            Console.WriteLine("=================================");
            Console.WriteLine($"       {title.ToUpper()}");
            Console.WriteLine("=================================");
        }

        public static void ShowDivider()
        {
            Console.WriteLine("-------------------");
        }

        public static void ShowSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
