using System;
using Database;
using FieldInput;
using Helpers;

namespace Formulationseach.Helpers
{
    public class FormulationSearch
    {
        public static void search()
        {
            using (PatientDBContext db = new PatientDBContext())
            {
                var patients = db.Patient.ToList();
                if (patients.Count == 0)
                {
                    global::Helpers.ConsoleHelper.ClearConsole();
                    Console.WriteLine("No registered patients.");
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
                    "Enter the ID of the patient to search (or 'back' to exit): ");
                if (idResult.Cancelled)
                {
                    Console.WriteLine("Search cancelled.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                var patient = db.Patient.Find(idResult.Value);
                if (patient == null)
                {
                    Console.WriteLine("Patient not found.");
                }
                else
                {
                    Console.WriteLine("Patient found:");
                    Console.WriteLine($"Name: {patient.Name}");
                    Console.WriteLine($"Lastname: {patient.Lastname}");
                    Console.WriteLine($"Age: {patient.Age}");
                    Console.WriteLine($"Personal ID: {patient.Personal}");
                    Console.WriteLine($"Disease: {patient.Disease}");
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
