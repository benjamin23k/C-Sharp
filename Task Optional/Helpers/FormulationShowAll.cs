using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Microsoft.EntityFrameworkCore;
using Helpers;

namespace ShowAll.Helpers
{
    public class FormulationShowAll
    {
        public static void Showallpatient()
        {
            using (PatientDBContext db = new PatientDBContext())
            {
                var Patient = db.Patient.AsNoTracking().ToList();

                if(Patient.Count == 0)
                {
                    global::Helpers.ConsoleHelper.ClearConsole();
                    Console.WriteLine("==============================");
                    Console.WriteLine("    No registered patients    ");
                    Console.WriteLine("==============================\n");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                global::Helpers.ConsoleHelper.ClearConsole();
                Console.WriteLine("========================================");
                Console.WriteLine("         REGISTERED PATIENTS            ");
                Console.WriteLine("========================================\n");

                foreach (var p in Patient)
                {
                    Console.WriteLine($" ID:       {p.Id}");
                    Console.WriteLine($" Name:     {p.Name} {p.Lastname}");
                    Console.WriteLine($" Age:      {p.Age}");
                    Console.WriteLine($" Personal ID:  {p.Personal}");
                    Console.WriteLine($" Disease:  {p.Disease}");
                    Console.WriteLine("----------------------------------------");
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}