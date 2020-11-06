using System;
using System.Collections.Generic;

namespace HousingEstateConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Housing Estate console. For help type help.");

            var editing = "";
            var running = true;

            var commands = new Dictionary<string, string>();

            commands.Add("quit", "quit an aplication");
            commands.Add("clear", "clears colsole");
            commands.Add("exit", "exit from editing if nothing is eddited then quit application");
            commands.Add("edit", "edit housing estate, flat etc.");
            commands.Add("create", "create new housing estate, flat etc.");
            commands.Add("save", "saves current housing flat on given path");
            commands.Add("load", "load housing flat from given path");

            while (running)
            {
                Console.Write(editing + ">");
                var input = Console.ReadLine();

                switch(input)
                {
                    case var x when x.Contains("help") :
                        if(x == "help")
                        {
                            foreach (var command in commands)
                                Console.WriteLine(command.Key + " " + command.Value);

                            Console.WriteLine("For extendet info use help command");
                        }

                        break;

                    case "quit" :
                        running = false;
                        break;

                    case "exit":
                        break;

                    case "clear" :
                        Console.Clear();
                        break;

                    case var x when x.Contains("create") :
                        break;

                    case var x when x.Contains("edit") :
                        break;

                    case "save" :
                        break;

                    case "load" :
                        break;
                }
            }
        }
    }
}
