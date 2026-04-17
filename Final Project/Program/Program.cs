using Order.Services;
using Add.Services;
using Modify.Services;
using Delete.Services;
using Search.Services;
using Show.Services;
using Count.Services;
using Database;

try
{
  Console.WriteLine("Connecting to database...");
  using (var context = new OrderDBContext())
  {
    if (context.Database.CanConnect())
    {
      Console.WriteLine("Successfully connected to the database!");
      Console.WriteLine("Press Enter to continue...");
      Console.ReadLine();
    }
    else
    {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine("ERROR: Could not connect to the database.");
      Console.ResetColor();
      Console.WriteLine("Please check your connection string and ensure SQL Server is running.");
      Console.WriteLine("Press Enter to exit...");
      Console.ReadLine();
      return;
    }
  }
  RunMenu();
}
catch (Exception ex)
{
  Console.ForegroundColor = ConsoleColor.Red;
  Console.WriteLine();
  Console.WriteLine("ERROR: Database connection failed.");
  Console.WriteLine(ex.Message);
  if (ex.InnerException != null) Console.WriteLine($"Inner: {ex.InnerException.Message}");
  Console.ResetColor();
  Console.WriteLine("Press Enter to exit...");
  Console.ReadLine();
}

static void RunMenu()
{
  bool running = true;

  while (running)
  {
    Console.Clear();
    Console.WriteLine("\n\n\n=================================");
    Console.WriteLine("       ORDER MANAGEMENT");
    Console.WriteLine("=================================");
    Console.WriteLine("  1. Show Orders");
    Console.WriteLine("  2. Add Order");
    Console.WriteLine("  3. Search Order");
    Console.WriteLine("  4. Modify Order");
    Console.WriteLine("  5. Delete Order");
    Console.WriteLine("  6. Count Orders");
    Console.WriteLine("  0. Exit");
    Console.WriteLine("=================================");
    Console.Write("\nSelect an option: ");

    string? input = Console.ReadLine();
    if (input is null)
    {
      Console.WriteLine("Input cancelled. Exiting program...");
      break;
    }

    input = input.Trim();

    switch (input)
    {
      case "1":
        ShowOrders.Show();
        break;
      case "2":
        AddOrders.Add();
        break;
      case "3":
        Search.Helpers.FormulationSearch.Search();
        break;
      case "4":
        ModifyOrders.Modify();
        break;
      case "5":
        DeleteOrders.Delete();
        break;
      case "6":
        CountOrders.Count();
        break;
      case "0":
        Console.Write("Are you sure you want to exit? (Y/N): ");
        string? exitConfirm = Console.ReadLine();
        if (exitConfirm?.Trim().ToUpper() == "Y")
        {
          Console.WriteLine("Exiting program...");
          running = false;
        }
        break;
      default:
        Console.WriteLine("Invalid option. Please try again.");
        break;
    }

    if (running && input != "0")
    {
        Console.WriteLine("\n[Press Enter to return to menu...]");
        Console.ReadLine();
    }
  }
}
