using System;
using Database;
using Patients;
using Validations;

namespace FormulationAdd
{
    using Helpers;

    public class Formulation
    {
        
        public static Patient? AddPatient()
        {
            Patient p = new Patient();

            Helpers.ConsoleHelper.ClearConsole();
            Console.WriteLine("Add new patient. Type 'back' in any field to cancel.\n");

            p.Name = LettersValidator.ReadOnlyLetters("Name: ", 1, 50);

           
           
            p.Lastname = LettersValidator.ReadOnlyLetters("Lastname: ", 1, 50);

         
         
            p.Age = NumbersValidator.ReadOnlyNumbers("Age (1-120): ", 1, 120);

            
            bool validPersonal = false;
            while (!validPersonal)
            {
                Console.Write("Personal ID (11 digits): ");
                string personalInput = Console.ReadLine()?.Trim() ?? "";
                
                if (personalInput.ToLower() == "back")
                {
                    Console.WriteLine("Addition cancelled.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return null;
                }

                if (personalInput.Length != 11)
                {
                    Console.WriteLine("Error: Personal ID must be exactly 11 digits. Please try again.");
                    continue;
                }

                if (!personalInput.All(char.IsDigit))
                {
                    Console.WriteLine("Error: Personal ID must contain only numbers. Please try again.");
                    continue;
                }

                if (!UniqueChecker.IsPersonalIdUnique(personalInput))
                {
                    Console.WriteLine("Error: This Personal ID already exists. Please enter a unique one.");
                    continue;
                }

                validPersonal = true;
                p.Personal = personalInput;
            }
         
           
            
            Console.Write("Disease: ");
            string disease = Console.ReadLine()?.Trim() ?? string.Empty;
            while (string.IsNullOrWhiteSpace(disease))
            {
                Console.WriteLine("Disease cannot be empty.");
                Console.Write("Disease: ");
                disease = Console.ReadLine()?.Trim() ?? string.Empty;
            }
            
            while (!disease.All(c => char.IsLetterOrDigit(c) || c == ' ' || c == '-' || c == ','))
            {
                Console.WriteLine("Only letters, numbers, spaces, hyphens and commas are allowed.");
                Console.Write("Disease: ");
                disease = Console.ReadLine()?.Trim() ?? string.Empty;
            }
            p.Disease = disease;

         
            Helpers.ConsoleHelper.ClearConsole();
            Console.WriteLine("\n--- Summary ---");
            Console.WriteLine($"Name: {p.Name}");
            Console.WriteLine($"Lastname: {p.Lastname}");
            Console.WriteLine($"Personal ID:{p.Personal} ");
            Console.WriteLine($"Age: {p.Age}");
            Console.WriteLine($"Disease: {p.Disease}");
            Console.WriteLine("-------------------");

            Console.Write("Are you sure you want to add this patient? (Y/N): ");
            string confirm = Console.ReadLine()?.Trim().ToUpper() ?? "";
            if (confirm != "Y")
            {
                Console.WriteLine("Addition cancelled.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return null;
            }

            Console.WriteLine("Patient ready to be saved.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            return p;
        }
    }
}

