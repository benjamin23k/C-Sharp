using Contacts.Add;
using Contacts.Modify;
using Contacts.Delete;
using Contacts.Show;
using Contacts.Search;
using Letters.validations;
using Read.validations;
using age.validations;
using Phone.validations;    
using Readgmail.validations;
using NumberPhone.validations;
using IsBest.validations;
using Contacts.Domain.Entities;
using Views.Contact;


bool running = true;


while (running)
{
    
     Console.WriteLine("\n======MENU==========");
     Console.WriteLine("1.Add Contact");
     Console.WriteLine("2.Show Contact");
     Console.WriteLine("3.Modify Contact");
     Console.WriteLine("4.Search Contact");
     Console.WriteLine("5.Delete Contact");
     Console.WriteLine("6.Exit");

    string? input = Console.ReadLine();
    if (!int.TryParse(input, out int option))
    {
        Console.WriteLine("Invalid input");
        continue;
    }

    switch (option)
    {
        case 1:
        {
            AddContact.Add();
            break;
        }
        case 2:
        {
                Views.Contact.View.Show();
                break;
        }

            
        case 3:
        {
                ModifyContact.Modify();
                break;
        }
        case 4: 
        {
                SearchContact.Search();
                break;
        }
            
        case 5: 
        {
                DeleteContact.Delete();
                break;
        }
            
        case 6:
        {
            running = false;
            Console.WriteLine("Exiting program...");


            break;
        }
        default:
        {
            Console.WriteLine("Option not valid");
            break;
        }
        
    }









}