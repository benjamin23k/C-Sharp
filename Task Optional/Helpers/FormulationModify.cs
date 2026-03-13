using Database;
using Patients;
using Validations;
using Helpers;
using FieldInput;

namespace Modify.Helpers
{
    public class FormulationModify
    {
        public static void ModifyPatient()
        {
            using (PatientDBContext db = new PatientDBContext())
            {
                var patients = db.Patient.ToList();
                if (patients.Count == 0)
                {
                    global::Helpers.ConsoleHelper.ClearConsole();
                    Console.WriteLine("No registered patients available to modify.");
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

                var idResult = FieldInputHelper.ReadIdField("Enter the ID of the patient to modify (or 'back' to exit): ");
                if (idResult.Cancelled)
                {
                    ShowCancelledAndWait();
                    return;
                }
                
                int id = idResult.Value;

                var patient = db.Patient.Find(id);
                if (patient == null)
                {
                    Console.WriteLine("Patient not found.");
                    ShowPressKey();
                    return;
                }

                Console.WriteLine($"\nModifying: {patient.Name} {patient.Lastname}");
                Console.WriteLine("Type 'back' to cancel or press Enter to keep current value.\n");

              
                Console.Write($"Current name: {patient.Name}. New name (press Enter to keep): ");
                string nameInput = Console.ReadLine()?.Trim() ?? "";
                if (nameInput.ToLower() == "back")
                {
                    ShowCancelledAndWait();
                    return;
                }
                if (!string.IsNullOrEmpty(nameInput))
                {
                    while (!nameInput.All(c => char.IsLetter(c) || c == ' '))
                    {
                        Console.WriteLine("Only letters are allowed. Please try again.");
                        Console.Write($"Current name: {patient.Name}. New name (press Enter to keep): ");
                        nameInput = Console.ReadLine()?.Trim() ?? "";
                        if (nameInput.ToLower() == "back")
                        {
                            ShowCancelledAndWait();
                            return;
                        }
                        if (string.IsNullOrEmpty(nameInput)) break;
                    }
                    if (!string.IsNullOrEmpty(nameInput))
                        patient.Name = nameInput;
                }

             
                Console.Write($"Current lastname: {patient.Lastname}. New lastname (press Enter to keep): ");
                string lastnameInput = Console.ReadLine()?.Trim() ?? "";
                if (lastnameInput.ToLower() == "back")
                {
                    ShowCancelledAndWait();
                    return;
                }
                if (!string.IsNullOrEmpty(lastnameInput))
                {
                    while (!lastnameInput.All(c => char.IsLetter(c) || c == ' '))
                    {
                        Console.WriteLine("Only letters are allowed. Please try again.");
                        Console.Write($"Current lastname: {patient.Lastname}. New lastname (press Enter to keep): ");
                        lastnameInput = Console.ReadLine()?.Trim() ?? "";
                        if (lastnameInput.ToLower() == "back")
                        {
                            ShowCancelledAndWait();
                            return;
                        }
                        if (string.IsNullOrEmpty(lastnameInput)) break;
                    }
                    if (!string.IsNullOrEmpty(lastnameInput))
                        patient.Lastname = lastnameInput;
                }

               
                Console.Write($"Current age: {patient.Age}. New age (press Enter to keep): ");
                string ageInput = Console.ReadLine()?.Trim() ?? "";
                if (ageInput.ToLower() == "back")
                {
                    ShowCancelledAndWait();
                    return;
                }
                if (!string.IsNullOrEmpty(ageInput))
                {
                    int ageValue;
                    while (!int.TryParse(ageInput, out ageValue) || ageValue < 1 || ageValue > 120)
                    {
                        Console.WriteLine("Please enter a valid number between 1 and 120.");
                        Console.Write($"Current age: {patient.Age}. New age (press Enter to keep): ");
                        ageInput = Console.ReadLine()?.Trim() ?? "";
                        if (ageInput.ToLower() == "back")
                        {
                            ShowCancelledAndWait();
                            return;
                        }
                        if (string.IsNullOrEmpty(ageInput)) break;
                    }
                    if (!string.IsNullOrEmpty(ageInput) && int.TryParse(ageInput, out ageValue))
                        patient.Age = ageValue;
                }

                while (true)
                {
                    Console.WriteLine($"Current Personal ID: {patient.Personal}. New Personal ID (press Enter to keep, 'back' to cancel): ");
                    string personalInput = Console.ReadLine()?.Trim() ?? "";
                    if (personalInput.ToLower() == "back")
                    {
                        ShowCancelledAndWait();
                        return;
                    }
                    if (string.IsNullOrEmpty(personalInput))
                    {
                        break; 
                    }
                    if (personalInput.Length != 11 || !personalInput.All(char.IsDigit))
                    {
                        Console.WriteLine("Error: Personal ID must be exactly 11 digits and contain only numbers. Please try again.");
                        continue;
                    }
                    if (!UniqueChecker.IsPersonalIdUnique(personalInput, patient.Id))
                    {
                        Console.WriteLine("Error: This Personal ID already exists (excluding current patient). Please try again.");
                        continue;
                    }
                    patient.Personal = personalInput;
                    break;
                }

                Console.Write($"Current disease: {patient.Disease}. New disease (press Enter to keep): ");
                string diseaseInput = Console.ReadLine()?.Trim() ?? "";
                if (diseaseInput.ToLower() == "back")
                {
                    ShowCancelledAndWait();
                    return;
                }
                if (!string.IsNullOrEmpty(diseaseInput))
                {
                    while (!diseaseInput.All(c => char.IsLetterOrDigit(c) || c == ' ' || c == '-' || c == ','))
                    {
                        Console.WriteLine("Only letters, numbers, spaces, hyphens and commas are allowed.");
                        Console.Write($"Current disease: {patient.Disease}. New disease (press Enter to keep): ");
                        diseaseInput = Console.ReadLine()?.Trim() ?? "";
                        if (diseaseInput.ToLower() == "back")
                        {
                            ShowCancelledAndWait();
                            return;
                        }
                        if (string.IsNullOrEmpty(diseaseInput)) break;
                    }
                    if (!string.IsNullOrEmpty(diseaseInput))
                        patient.Disease = diseaseInput;
                }



                Console.Write("Are you sure you want to modify this patient? (Y/N): ");
                string confirm = Console.ReadLine()?.Trim().ToUpper() ?? "";
                if (confirm != "Y")
                {
                    ShowCancelledAndWait();
                    return;
                }

                db.SaveChanges();
                Console.WriteLine("Patient updated successfully.");
                ShowPressKey();
            }
        }

        private static void ShowCancelledAndWait()
        {
            Console.WriteLine("Modification cancelled.");
            ShowPressKey();
        }

        private static void ShowPressKey()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}

