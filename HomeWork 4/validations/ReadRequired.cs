using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Read.validations
{
    public class Validation  
 { 
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
   }
}