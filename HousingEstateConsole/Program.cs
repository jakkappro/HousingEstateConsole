using System;
using System.Collections.Generic;
using System.Linq;

namespace HousingEstateConsole
{
    internal static class Program
    {
        private static string GetInput(string message, bool isInt = false)
        {
            Console.Write(message);
            var buffer = Console.ReadLine();

            if (isInt)
            {
                while (string.IsNullOrEmpty(buffer) || !int.TryParse(buffer, out _))
                {
                    Console.Write(message);
                    buffer = Console.ReadLine();
                }
            }
            else
            {
                while (string.IsNullOrEmpty(buffer))
                {
                    Console.Write(message);
                    buffer = Console.ReadLine();
                }
            }
            
            return buffer;
        }
        
        private static void Main()
        {
            var input = "";
            var editing = 0;

            CityManager.Init("default");

            while (input != "quit")
            {
                Console.WriteLine(
                    $"U are editing {CityManager._showAble.GetType().ToString().Substring(21)} with name {CityManager._showAble.GetWriteName()} now.");
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
                                    var buffer = new List<object>();

                                    if (splitInput.Length == 8)
                                    {
                                        for (var i = 1; i < splitInput.Length; i++)
                                            buffer.Add(splitInput[i]);

                                        CityManager.Create(buffer);

                                        break;
                                    }

                                    buffer.Add(GetInput("Street: "));

                                    buffer.Add(GetInput("Entrance number:", true));

                                    buffer.Add(GetInput("Block of flats number: ", true));

                                    buffer.Add(GetInput("Floors: ", true));

                                    buffer.Add(GetInput("Flats per floor: ", true));

                                    buffer.Add(GetInput("Area: ", true));
                                    
                                    buffer.Add(GetInput("Rooms: ", true));

                                    CityManager.Create(buffer);

                                    break;

                                case "exit":
                                    input = "quit";

                                    break;

                                case "change":
                                    if (splitInput.Length != 3)
                                    {
                                        CityManager.Change(splitInput[1], splitInput[2]);

                                        break;
                                    }

                                    Console.WriteLine(
                                        "Only changeable variable is name. If you want change name type name new_name");

                                    splitInput = Console.ReadLine().Split(" ");

                                    if (splitInput.Length != 2)
                                        break;

                                    CityManager.Change(splitInput[0], splitInput[1]);

                                    break;

                                case "switch":
                                    if (splitInput.Length != 2)
                                    {
                                        Console.WriteLine("Next possible hops:");
                                        Console.WriteLine(CityManager.GetDir(typeof(HousingEstate)));
                                        var hop = Console.ReadLine();
                                        editing = CityManager.Switch(hop) ? 1 : 0;

                                        break;
                                    }

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
                                    {
                                        Console.WriteLine("You can change name or number of current block of flats. Type changing chane");
                                        splitInput = Console.ReadLine().Split(" ");

                                        if (splitInput.Length != 2)
                                            break;

                                        CityManager.Change(splitInput[0], splitInput[1]);

                                        break;
                                    }

                                    CityManager.Change(splitInput[1], splitInput[2]);

                                    break;

                                case "switch":
                                    if (splitInput.Length != 2)
                                    {
                                        Console.WriteLine("Next possible hops:");
                                        Console.WriteLine(CityManager.GetDir(typeof(BlockOfFlats)));
                                        var hop = Console.ReadLine();
                                        editing = CityManager.Switch(hop) ? 1 : 0;

                                        break;
                                    }

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
                                

                                case "switch":
                                    if (splitInput.Length != 2)
                                    {
                                        Console.WriteLine("Next possible hops:");
                                        Console.WriteLine(CityManager.GetDir(typeof(Entrance)));
                                        var hop = Console.ReadLine();
                                        editing = CityManager.Switch(hop) ? 1 : 0;

                                        break;
                                    }

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
                                    var buffer = new List<object>();

                                    if (splitInput.Length != 4)
                                    {
                                        buffer.Add(GetInput("First name: "));

                                        buffer.Add(GetInput("Second name: "));

                                        buffer.Add(GetInput("Age: ", true));


                                        break;
                                    }


                                    for (var i = 1; i < splitInput.Length; i++)
                                        buffer.Add(splitInput[i]);

                                    CityManager.Create(buffer);

                                    break;

                                case "exit":
                                    CityManager.Exit();

                                    break;

                                case "change":
                                    if (splitInput.Length != 3)
                                    {
                                        Console.WriteLine("You can change area or rooms of this flat. You can change it by writing changing change");
                                        splitInput = Console.ReadLine()?.Split(" ");

                                        if (splitInput.Length != 2)
                                            break;
                                        
                                        CityManager.Change(splitInput[0], splitInput[1]);
                                        
                                        break;
                                    }

                                    CityManager.Change(splitInput[1], splitInput[2]);

                                    break;

                                case "switch":
                                    if (splitInput.Length != 2)
                                    {
                                        Console.WriteLine("Next possible hops:");
                                        Console.WriteLine(CityManager.GetDir(typeof(Flat)));
                                        var hop = Console.ReadLine();
                                        editing = CityManager.Switch(hop) ? 1 : 0;

                                        break;
                                    }

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
                                    {
                                        Console.WriteLine("You can change firstName or secondName or age of this person. You can change it by writing changing change");
                                        splitInput = Console.ReadLine().Split(" ");

                                        if (splitInput.Length != 2)
                                            break;
                                        
                                        CityManager.Change(splitInput[0], splitInput[1]);
                                        
                                        break;
                                    }

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