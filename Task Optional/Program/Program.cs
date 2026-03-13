using System;
using Database;
using Patients;

try
{
  Console.WriteLine("Connecting to database...");
  using (var context = new PatientDBContext())
  {
    if (context.Database.CanConnect())
    {
      Console.WriteLine("Successfully connected to the database!");
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }
    else
    {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine("ERROR: Could not connect to the database.");
      Console.ResetColor();
      Console.WriteLine("Please check your connection string and ensure SQL Server is running.");
      Console.WriteLine("Press any key to exit...");
      Console.ReadKey();
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
  Console.ResetColor();
  Console.WriteLine("Press any key to exit...");
  Console.ReadKey();
}

static void RunMenu()
{
  bool running = true;

  while (running)
  {
    Helpers.ConsoleHelper.ClearConsole();
    Console.WriteLine("=== Patient Management ===");
    Console.WriteLine("1. Add Patient");
    Console.WriteLine("2. Show Patients");
    Console.WriteLine("3. Modify Patient");
    Console.WriteLine("4. Delete Patient");
    Console.WriteLine("5. Search Patient");
    Console.WriteLine("0. Exit");
    Console.Write("Select an option: ");

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
        var newPatient = FormulationAdd.Formulation.AddPatient();
        if (newPatient != null)
        {
          Add.Patients.AddPatient.Add(newPatient);
          Console.WriteLine("Patient added successfully!");
          Console.WriteLine("Press any key to continue...");
          Console.ReadKey();
        }
        break;
      case "2":
        ShowAll.Helpers.FormulationShowAll.Showallpatient();
        break;
      case "3":
        Modify.Helpers.FormulationModify.ModifyPatient();
        break;
      case "4":
        Delete.Helpers.FormulationDelete.Delete();
        break;
      case "5":
        Formulationseach.Helpers.FormulationSearch.search();
        break;
      case "0":
      case "q":
      case "Q":
        Console.Write("Are you sure you want to exit? (Y/N): ");
        string? exitConfirm = Console.ReadLine();
        if (exitConfirm?.Trim().ToUpper() == "Y")
        {
          running = false;
        }
        break;
      default:
        Console.WriteLine("Invalid option. Please try again.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        break;
    }
  }
}
