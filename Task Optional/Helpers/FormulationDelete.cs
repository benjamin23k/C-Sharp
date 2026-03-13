using System;
using Database;
using FieldInput;
using Helpers;

namespace Delete.Helpers
{
    public static class FormulationDelete
    {
        public static void Delete()
        {
            using (var db = new PatientDBContext())
            {
                var patients = db.Patient.ToList();
                if (patients.Count == 0)
                {
                    global::Helpers.ConsoleHelper.ClearConsole();
                    Console.WriteLine("No registered patients available to delete.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                global::Helpers.ConsoleHelper.ClearConsole();
                Console.WriteLine("========================================");
                Console.WriteLine("         REGISTERED PATIENTS            ");
                Console.WriteLine("========================================\n");
                foreach (var p in patients)
                {
                    Console.WriteLine($" ID:       {p.Id}");
                    Console.WriteLine($" Name:     {p.Name} {p.Lastname}");
                    Console.WriteLine($" Age:      {p.Age}");
                    Console.WriteLine($" Personal ID:  {p.Personal}");
                    Console.WriteLine($" Disease:  {p.Disease}");
                    Console.WriteLine("----------------------------------------");
                }

                Console.WriteLine();

                var idResult = FieldInputHelper.ReadIdField(
                    "Enter the ID of the patient to delete (or 'back' to exit): ");
                if (idResult.Cancelled)
                {
                    Console.WriteLine("Deletion cancelled.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                var patient = db.Patient.Find(idResult.Value);
                if (patient == null)
                {
                    Console.WriteLine("Patient not found.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                var confirmResult = FieldInputHelper.ReadConfirm(
                    $"Are you sure you want to delete {patient.Name} {patient.Lastname}? (Y/N): ");
                if (confirmResult.Cancelled || confirmResult.Value != true)
                {
                    Console.WriteLine("Deletion cancelled.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

              
                Console.WriteLine("Patient deleted successfully.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}

