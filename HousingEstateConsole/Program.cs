using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

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
                    $"U are editing {CityManager.ShowAble.GetType().ToString().Substring(21)} with name {CityManager.ShowAble.GetWriteName()} now.");
                //TODO: list possible commands
                Console.Write(">");
                input = Console.ReadLine();

                if (input != null)
                {
                    var splitInput = input.Split(" ");

                    switch (splitInput[0])
                    {
                        case "resident":
                            if (splitInput.Length != 2)
                                break;
                            
                            var names = new List<string>();
                            while (CityManager.ShowAble.GetType() != typeof(HousingEstate))
                            {
                                names.Add(CityManager.ShowAble.GetWriteName());
                                CityManager.Exit();
                            }

                            var housing = (HousingEstate) CityManager.ShowAble;
                            foreach (var people in housing.GetHousingResidents()
                                .Where(p => p.GetFullName() == splitInput[1]))
                            {
                                Console.WriteLine(people.Show());
                            }

                            names.Reverse();

                            foreach (var name in names)
                            {
                                CityManager.Switch(name);
                            }

                            Console.ReadKey();

                            break;
                        
                        case "exit":
                            try
                            {
                                CityManager.Exit();
                                editing--;
                            }
                            catch (Exception)
                            {
                                input = "quit";
                            }

                            break;
                        
                        case "show":
                            Console.WriteLine(CityManager.ShowAble.Show());
                            Console.ReadKey();
                        
                            break;
                        
                        case "switch":
                            if (splitInput.Length != 2)
                            {
                                Console.WriteLine("Next possible hops:");
                                Console.WriteLine(CityManager.ShowAble.GetStructure());
                                var hop = Console.ReadLine();
                                editing = CityManager.Switch(hop) ? editing + 1 : editing;

                                break;
                            }

                            editing = CityManager.Switch(splitInput[1]) ? editing + 1 : editing;

                            break;
                        
                        case "save":
                            var nnames = new List<string>();
                            while (CityManager.ShowAble.GetType() != typeof(HousingEstate))
                            {
                                nnames.Add(CityManager.ShowAble.GetWriteName());
                                CityManager.Exit();
                            }

                            var serializer = new XmlSerializer(typeof(HousingEstate));
                            var saving = (HousingEstate) CityManager.ShowAble;

                            if(File.Exists(@"C:\test.xml"))
                                File.Delete(@"C:\test.xml");

                            var tw = new StreamWriter(@"C:\test.xml");
                            serializer.Serialize(tw, saving);
                            //TODO: remove all references -> save file -> restore all references somehow :)

                            tw.Close();

                            nnames.Reverse();

                            foreach (var name in nnames)
                            {
                                CityManager.Switch(name);
                            }
                            
                            break;
                        
                        default:
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

                                            buffer.Add(GetInput("Entrance number: ", true));

                                            buffer.Add(GetInput("Block of flats number: ", true));

                                            buffer.Add(GetInput("Floors: ", true));

                                            buffer.Add(GetInput("Flats per floor: ", true));

                                            buffer.Add(GetInput("Area: ", true));
                                    
                                            buffer.Add(GetInput("Rooms: ", true));

                                            CityManager.Create(buffer);

                                            break;

                                        case "change":
                                            if (splitInput.Length == 3)
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
                                    }

                                    break;

                                case 1:

                                    switch (splitInput[0])
                                    {
                                        case "create":
                                            CityManager.Create(null);

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

                                                CityManager.Create(buffer);

                                                break;
                                            }


                                            for (var i = 1; i < splitInput.Length; i++)
                                                buffer.Add(splitInput[i]);

                                            CityManager.Create(buffer);

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
                                    }

                                    break;

                                case 4:

                                    switch (splitInput[0])
                                    {
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
                                    }

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
