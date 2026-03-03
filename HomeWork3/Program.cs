using System.Text.RegularExpressions;
using System.Net.Mail;

Console.WriteLine("===== CONTACT LIST =====");

bool running = true;

List<int> ids = new();

Dictionary<int, string> names = new();
Dictionary<int, string> lastnames = new();
Dictionary<int, string> addresses = new();
Dictionary<int, string> telephones = new();
Dictionary<int, string> emails = new();
Dictionary<int, int> ages = new();
Dictionary<int, bool> bestFriends = new();




while (running)
{
    Console.WriteLine("\n====== MENÚ ======");
    Console.WriteLine("1. Add Contact");
    Console.WriteLine("2. Show All Contacts");
    Console.WriteLine("3. Search For Contact (by ID)");
    Console.WriteLine("4. Modify Contact");
    Console.WriteLine("5. Delete Contact");
    Console.WriteLine("6. Exit");

    int option = ReadNumber("Select an Option: ");

    switch (option)
    {
        case 1: AddContact(); break;
        case 2: ShowAllContacts(); break;
        case 3: SearchContact(); break;
        case 4: ModifyContact(); break;
        case 5: DeleteContact(); break;
        case 6: running = false; break;
    }
}





bool Confirm(string message)
{
    while (true)
    {
        Console.Write($"{message} (Y/N): ");
        string input = Console.ReadLine()!.Trim().ToUpper();

        if (input == "Y") return true;
        if (input == "N") return false;
    }
}

string ReadRequired(string message)
{
    string input;

    do
    {
        Console.Write(message);
        input = Console.ReadLine()!.Trim();
    }
    while (string.IsNullOrWhiteSpace(input));

    return input;
}

string ReadOnlyLetters(string message, int min, int max)
{
    while (true)
    {
        min = 2;
        max = 14;
        Console.Write(message);
        string input = Console.ReadLine()!.Trim();

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("The Field Cannot be empty");
            continue;
        }

        if (input.Length < min || input.Length > max)
        {
            Console.WriteLine($"Between {min} And {max} Characters");
            continue;  
        }

        if (input.All(c => char.IsLetter(c) || c == ' '))
            return input;

        Console.WriteLine("Only Letters Are Allowed");
    }
}

string ReadPhone(string message)
{
    while (true)
    {
        Console.Write(message);
        string input = Console.ReadLine()!.Trim();

        if (string.IsNullOrWhiteSpace(input))
            continue;

        if (!input.All(char.IsDigit))
        {
            Console.WriteLine(" Invalid Phone Example: 8099162344");
            continue;
        }

        if (input.Length < 10 || input.Length > 11)
        {
            Console.WriteLine(" Invalid Phone Example: 8099162344");
            continue;
        }

        return input;
    }
}

string ReadEmail(string message)
{
    string input = "";

    bool active = true;
    while (active)
    {

        try
        {
            Console.Write(message);
            input = Console.ReadLine()!;
            var mail = new MailAddress(input);
            active = false;
        }
        catch (FormatException ex)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.WriteLine($"Try again to write the email using format valid [ej: example@gmail.com");
            Console.ResetColor();
            Console.WriteLine(" email. Try to again with format valid", "ana@gmail.com");
        }

    }
    return input;
}


int ReadNumber(string message)
{
    while (true)
    {
        Console.Write(message);

        if (int.TryParse(Console.ReadLine(), out int value))
            return value;
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(" Try Again Example: 849927382");
        Console.ResetColor();
    }
}

int ReadAge(string message)
{
    while (true)
    {
        Console.Write(message);

        if (int.TryParse(Console.ReadLine(), out int age) && age >= 1 && age <= 119)
            return age;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(" Invalid Age.");
        Console.ResetColor();
    }
}

bool ReadBestFriend()
{
    while (true)
    {
        Console.Write("¿BestFriend? (1=Yes, 2=No): ");
        string input = Console.ReadLine()!.Trim();

        if (input == "1" || input == "Yes") return true;
        if (input == "2" ||  input== "No") return false;
    }
}




         void AddContact()
{
    string name = ReadOnlyLetters("Name: ", 2, 50);
    string lastname = ReadOnlyLetters("Lastname: ", 2, 50);
    string address = ReadRequired("Address: ");

    string phone;                                                                                                     
    do
    {                                                        
        phone = ReadPhone("Phone: ");

        if (telephones.ContainsValue(phone))
            Console.WriteLine(" That Phone Already Exists.");

    } while (telephones.ContainsValue(phone));

    string email = ReadEmail("Email: ");
    int age = ReadAge("Age: ");
    bool best = ReadBestFriend();

    Console.WriteLine("\n----- SUMMARY -----");
    Console.WriteLine($"{name} {lastname}");
    Console.WriteLine($"Address: {address}");
    Console.WriteLine($"Phone: {phone}");
    Console.WriteLine($"Email: {email}");
    Console.WriteLine($"Age: {age}");
    Console.WriteLine($"BestFriend: {(best ? "Yes" : "No")}");

    if (!Confirm("¿Save Contact?"))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(" Canceled.");
        Console.ResetColor();
        return;
    }

    int id = ids.Count == 0 ? 1 : ids.Max() + 1;

    ids.Add(id);
    names[id] = name;
    lastnames[id] = lastname;
    addresses[id] = address;
    telephones[id] = phone;
    emails[id] = email;
    ages[id] = age;
    bestFriends[id] = best;
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("\nContact Created.");
    Console.ResetColor();
}




void ShowAllContacts()
{
    if (ids.Count == 0)
    {
        Console.WriteLine("There are no contacts.");
        return;
    }
    Console.Clear();
    Console.WriteLine("\n===== CONTACT LIST =====\n");

    
    Console.WriteLine(
        "ID".PadRight(5) +
        "Name".PadRight(15) +
        "Lastname".PadRight(15) +
        "Phone".PadRight(15) +
        "Age".PadRight(6) +
        "Friend"
    );

    Console.WriteLine(new string('-', 70));

    
    foreach (var id in ids.OrderBy(x => x))
    {
        Console.WriteLine(
            id.ToString().PadRight(5) +
            names[id].PadRight(15) +
            lastnames[id].PadRight(15) +
            telephones[id].PadRight(15) +
            ages[id].ToString().PadRight(6) +
            (bestFriends[id] ? "Yes" : "No")
        );
    }

    Console.WriteLine();
}




void SearchContact()
{
    Console.Clear();
    if (ids.Count == 0)
    {
        Console.WriteLine("There are no contacts.");
        return;
    }

    ShowAllContacts();

    int id = ReadNumber("ID To seach for?: ");

    if (!ids.Contains(id))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(" Does not exist.");
        Console.ResetColor();
        return;
    }

    Console.WriteLine("\n===== CONTACT =====");
    Console.WriteLine($"Name: {names[id]} {lastnames[id]}");
    Console.WriteLine($"Address: {addresses[id]}");
    Console.WriteLine($"Phone: {telephones[id]}");
    Console.WriteLine($"Email: {emails[id]}");
    Console.WriteLine($"Age: {ages[id]}");
    Console.WriteLine($"BestFriend: {(bestFriends[id] ? "Yes" : "No")}");
}




void ModifyContact()
{
    Console.Clear();
    if (ids.Count == 0) return;

    ShowAllContacts();

    int id = ReadNumber("ID to modify?: ");

    if (!ids.Contains(id))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(" Does not exist.");
        Console.ResetColor();
        return;
    }

    string newName = ReadOnlyLetters("New name: ", 2, 50);
    string newLastname = ReadOnlyLetters("New Lastname: ", 2, 50);
    string newAddress = ReadRequired("New Address: ");
    string newPhone = ReadPhone("New Phone: ");
    string newEmail = ReadEmail("New Email: ");
    int newAge = ReadAge("New Age: ");
    bool newBest = ReadBestFriend();

    Console.WriteLine("\n----- NEW DATA -----");
    Console.WriteLine($"{newName} {newLastname}");

    if (!Confirm("¿Apply changes?"))
        return;

    names[id] = newName;
    lastnames[id] = newLastname;
    addresses[id] = newAddress;
    telephones[id] = newPhone;
    emails[id] = newEmail;
    ages[id] = newAge;
    bestFriends[id] = newBest;
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine(" Modified.");
    Console.ResetColor();
}



void DeleteContact()
{
    Console.Clear();
    if (ids.Count == 0) return;

    ShowAllContacts();

    int id = ReadNumber("ID To Delete: ");

    if (!ids.Contains(id))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(" Does not exist.");
        Console.ResetColor();
        return;
    }

    if (!Confirm($"¿Eliminate {names[id]} {lastnames[id]}?"))
        return;

    ids.Remove(id);
    names.Remove(id);
    lastnames.Remove(id);
    addresses.Remove(id);
    telephones.Remove(id);
    emails.Remove(id);
    ages.Remove(id);
    bestFriends.Remove(id);

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine(" removed.");
    Console.ResetColor();
}
