
//Name Wilson Benjamin  Registration 2025-0968   

while (true){
Console.WriteLine("Enter a number (or 'exit' to quit);");
string entry = Console.ReadLine()!;

if(entry.ToLower()== "exit"){
    break;
}
if(int.TryParse(entry, out int number)){
     
     if(number == 0)
    {
        Console.WriteLine($"The number 0 is neutral");
    }
    else if (number % 2 == 0)
    {Console.WriteLine($"the number {number} is even");
    }
    else
    {
    Console.WriteLine($"the number {number} is odd");
    }
    }
    else
    {
        Console.WriteLine("Invalid number or character");
    }
}


    
