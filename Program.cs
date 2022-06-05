using System;

namespace Pattiyaan
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var interpreter = new Interpreter();
            var cont = true;

            while(cont)
            {
                try
                {
                    // Prompt the user and get raw input.
                    Console.Write("> ");
                    var rawInput = Console.ReadLine();

                    interpreter.Execute(rawInput);
                    
                    Console.WriteLine();
                } 
                catch (InterpretationException e)
                {
                    // If it's an exception related to interpretation of input,
                    // don't stop the program, but inform the user a problem has occured.
                    Console.WriteLine(e.Message);                            
                }
                catch (Exception e)
                {
                    // If it's anything else, end the program.
                    cont = false;
                    
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Exiting program.");
                }
            }
        }
    }
}