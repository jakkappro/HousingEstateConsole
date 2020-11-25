using System;
using System.Collections.Generic;
using System.Linq;

namespace HousingEstateConsole
{
    internal static class Program
    {
        private static void Main()
        {
            var input = "";
            var editing = 0;
            
            CityManager.Init("default");

            while (input != "quit")
            {
                Console.WriteLine($"U are editing {CityManager._showAble.GetType().ToString().Substring(21)} with name {CityManager._showAble.GetWriteName()} now.");
                //TODO: list possible commands
                Console.Write(">");
                input = Console.ReadLine();
                
                if (input != null)
                {
                    var splitInput = input.Split(" ");
                    
                    if (splitInput[0] == "resident" && splitInput.Length == 2)
                    {
                        var names = new List<string>();
                        while (CityManager._showAble.GetType() != typeof(HousingEstate))
                        {
                            names.Add(CityManager._showAble.GetWriteName());
                            CityManager.Exit();
                        }

                        var buffer = (HousingEstate) CityManager._showAble;
                        foreach (var people in buffer.GetHousingResidents()
                            .Where(p => p.GetFullName() == splitInput[1]))
                        {
                            Console.WriteLine(people.GetData());
                        }

                        names.Reverse();

                        foreach (var name in names)
                        {
                            CityManager.Switch(name);
                        }

                        Console.ReadKey();
                    }
                    
                    switch (editing)
                    {
                        case 0:

                            switch (splitInput[0])
                            {
                                case "create":
                                    if (splitInput.Length == 6)
                                    {
                                        var buffer = new List<object>();
                                        for(var i = 1; i < splitInput.Length; i++)
                                            buffer.Add(splitInput[i]);//TODO: get variables from user if he doesn't give them
                                        
                                        CityManager.Create(buffer);
                                    }

                                    break;
                                
                                case "exit":
                                    input = "quit";
                                    
                                    break;
                                
                                case "change":
                                    if (splitInput.Length != 3)
                                        break; //TODO: show possible changes
                                    
                                    CityManager.Change(splitInput[1], splitInput[2]);

                                    break;
                                
                                case "switch":
                                    if (splitInput.Length != 2)
                                        break;//TODO: get next hop
                                    
                                    editing = CityManager.Switch(splitInput[1]) ? 1 : 0;

                                    break;
                                
                                case "show":
                                    CityManager.Show();
                                    Console.ReadKey();

                                    break;
                            }

                            break;
                    
                        case 1:
                            
                            switch (splitInput[0])
                            {
                                case "create":
                                    CityManager.Create(null);

                                    break;
                                
                                case "exit":
                                    CityManager.Exit();
                                    
                                    break;
                                
                                case "change":
                                    if (splitInput.Length != 3)
                                        break; //TODO: show possible changes
                                    
                                    CityManager.Change(splitInput[1], splitInput[2]);

                                    break;
                                
                                case "switch":
                                    if (splitInput.Length != 2)
                                        break;//TODO: get next hop
                                    
                                    editing = CityManager.Switch(splitInput[1]) ? 2 : 1;

                                    break;
                                
                                case "show":
                                    CityManager.Show();
                                    Console.ReadKey();

                                    break;
                            }

                            break;
                    
                        case 2:
                            
                            switch (splitInput[0])
                            {
                                case "exit":
                                    CityManager.Exit();
                                    
                                    break;
                                
                                case "change":
                                    if (splitInput.Length != 3)
                                        break; //TODO: show possible changes
                                    
                                    CityManager.Change(splitInput[1], splitInput[2]);

                                    break;
                                
                                case "switch":
                                    if (splitInput.Length != 2)
                                        break;//TODO: get next hop
                                    
                                    editing = CityManager.Switch(splitInput[1]) ? 3 : 2;

                                    break;
                                
                                case "show":
                                    CityManager.Show();
                                    Console.ReadKey();

                                    break;
                            }

                            break;

                        case 3:
                            
                            switch (splitInput[0])
                            {
                                case "create":
                                    if (splitInput.Length != 4)
                                        break;
                                    
                                    var buffer = new List<object>();
                                    for(var i = 1; i < splitInput.Length; i++)
                                        buffer.Add(splitInput[i]);
                                    CityManager.Create(buffer);

                                    break;
                                
                                case "exit":
                                    CityManager.Exit();
                                    
                                    break;
                                
                                case "change":
                                    if (splitInput.Length != 3)
                                        break; //TODO: show possible changes
                                    
                                    CityManager.Change(splitInput[1], splitInput[2]);

                                    break;
                                
                                case "switch":
                                    if (splitInput.Length != 2)
                                        break;//TODO: get next hop
                                    
                                    editing = CityManager.Switch(splitInput[1]) ? 4 : 3;

                                    break;
                                
                                case "show":
                                    CityManager.Show();
                                    Console.ReadKey();

                                    break;
                            }

                            break;

                        case 4:
                            
                            switch (splitInput[0])
                            {
                                case "exit":
                                    CityManager.Exit();
                                    
                                    break;
                                
                                case "change":
                                    if (splitInput.Length != 3)
                                        break; //TODO: show possible changes
                                    
                                    CityManager.Change(splitInput[1], splitInput[2]);

                                    break;

                                case "show":
                                    CityManager.Show();
                                    Console.ReadKey();

                                    break;
                            }

                            break;
                    }
                }
                
                Console.Clear();
            }
        }
    }
}

//TODO: dir/ls, make property of some variables