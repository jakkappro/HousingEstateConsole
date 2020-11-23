using System;
using System.Collections.Generic;
using System.Linq;

namespace HousingEstateConsole
{
    internal static class Program
    {
        private static List<HousingEstate> _housingEstates;

        private static bool _running;

        private static string _editing;

        private static int _flatIndex;
        private static int _personIndex;
        private static int _housingIndex;
        private static int _entranceIndex;
        private static int _blockOfFlatsIndex;

        private static void UpdateIndexes()
        {
            _flatIndex = -1;
            _personIndex = -1;
            _housingIndex = -1;
            _entranceIndex = -1;
            _blockOfFlatsIndex = -1;


            var indexNames = _editing.Split(">");

            if (indexNames.Length > 2)
            {
                foreach (var estate in _housingEstates.Where(estate => estate.Name == indexNames[1]))
                    _housingIndex = _housingEstates.IndexOf(estate);
            }

            if (indexNames.Length > 3)
            {
                foreach (var blockOfFlats in _housingEstates[_housingIndex].BlockOfFlats.Where(estate => estate.BlockOfFlatsNumber == int.Parse(indexNames[2])))
                    _blockOfFlatsIndex = _housingEstates[_housingIndex].BlockOfFlats.IndexOf(blockOfFlats);
            }
            if (indexNames.Length > 4)
            {
                foreach (var entrance in _housingEstates[_housingIndex].BlockOfFlats[_blockOfFlatsIndex].GetEntrances().Where(estate => estate.EntranceNumber == int.Parse(indexNames[3])))
                    _entranceIndex = _housingEstates[_housingIndex].BlockOfFlats[_blockOfFlatsIndex].GetEntrances().IndexOf(entrance);
            }
            if (indexNames.Length > 5)
            {
                foreach (var flat in _housingEstates[_housingIndex].BlockOfFlats[_blockOfFlatsIndex].GetEntrances()[_entranceIndex].Flats.Where(flat => flat.FlatNumber == int.Parse(indexNames[4])))
                    _flatIndex = _housingEstates[_housingIndex].BlockOfFlats[_blockOfFlatsIndex].GetEntrances()[_entranceIndex].Flats.IndexOf(flat);
            }
            if (indexNames.Length > 6)
            {
                foreach (var person in _housingEstates[_housingIndex].BlockOfFlats[_blockOfFlatsIndex].GetEntrances()[_entranceIndex].Flats[_flatIndex].Residents.Where(person => person.GetFullName() == indexNames[5]))
                    _flatIndex = _housingEstates[_housingIndex].BlockOfFlats[_blockOfFlatsIndex].GetEntrances()[_entranceIndex].Flats[_flatIndex].Residents.IndexOf(person);
            }
        }

        private static void Help(string[] buffer)
        {
            var overloads = new List<string>();
            for (var i = 1; i < buffer.Length; i++)
                overloads.Add(buffer[i]);
            
            if (overloads.Count == 0)
            {
                Console.WriteLine("Global commands:");
                Console.WriteLine("help  -> show information about command or all commands.");
                Console.WriteLine("clear -> clears console");
                Console.WriteLine("cls   -> clears console");
                Console.WriteLine("exit  -> switch one unit behind or end a program");//TODO: Check if works properly

                Console.WriteLine("\nDefault commands:"); //TODO: create more reasonable name for group of commands
                Console.WriteLine("create -> creates new housing estate");
                Console.WriteLine("load   -> loads housing estate from a file");//TODO: loading doesn't work
                Console.WriteLine("switch -> switch to already existing housing estate");

                Console.WriteLine("\nHousing estate commands:");
                Console.WriteLine("create -> creates new block of flats");
                Console.WriteLine("switch -> switch to already existing block of flats");
                Console.WriteLine("remove -> removes block of flats");
                Console.WriteLine("save   -> saves current housing estate");//TODO: saving doesn't work
                Console.WriteLine("name   -> show name of housing estate");
                Console.WriteLine("resident -> show information about resident");

                Console.WriteLine("\nBlock of house commands:");
                Console.WriteLine("create           -> creates new entrance");
                Console.WriteLine("switch           -> switch to already existing entrance");
                Console.WriteLine("number           -> show or change block of flats number");
                Console.WriteLine("street           -> show or change block of flats street");
                Console.WriteLine("floors           -> show or change block of flats floors");
                Console.WriteLine("flats_per_floors -> show block of flats flats per floor");

                Console.WriteLine("\nEntrance commands:");
                Console.WriteLine("switch -> switch to flat");
                Console.WriteLine("number -> show entrance number");

                Console.WriteLine("\nFlat commands:");
                Console.WriteLine("create -> creates new resident");
                Console.WriteLine("switch -> switch to already existing resident");
                Console.WriteLine("remove -> removes resident");
                Console.WriteLine("number -> show flat number");
                Console.WriteLine("floor  -> show flat floor");

                Console.WriteLine("\nResident commands:");
                
            }
            else
            {
                //TODO: create definition and usage for commands maybe new class for command
            }
        }

        private static void Default(string[] overloads)
        {
            switch (overloads[0])
            {
                case "create":
                    if (overloads.Length == 2)
                        _housingEstates.Add(new HousingEstate(overloads[1]));
                    
                    else
                    {
                        Console.Write("Please insert name of housing estate: ");
                        var name = Console.ReadLine();
                        _housingEstates.Add(new HousingEstate(name));
                    }
                    
                    break;
                
                case "load":
                    break;
                
                case "switch":
                    if (overloads.Length != 2)
                        break;

                    foreach (var housing in _housingEstates.Where(housing => housing.Name == overloads[1]))
                        _editing += housing.Name + ">";

                    break;
            }
        }

        private static void Housing(string[] overloads)
        {
            switch (overloads[0])
            {
                case "create": //TODO:Better control of user input, creates first entrance automaticly
                    var lel = _housingEstates[_housingIndex];
                    if (overloads.Length == 6)
                    {
                        _housingEstates[_housingIndex].Add(new BlockOfFlats(overloads[1], int.Parse(overloads[2]), int.Parse(overloads[3]), int.Parse(overloads[4]), int.Parse(overloads[5]), ref lel));
                    }
                    
                    else
                    {
                        Console.Write("Street: ");
                        var street = Console.ReadLine();
                        
                        Console.Write("First entrance number: ");
                        var en = int.Parse(Console.ReadLine()!);
                        
                        Console.Write("Block of flats number: ");
                        var bofn = int.Parse(Console.ReadLine()!);
                        
                        Console.Write("Floors: ");
                        var fl = int.Parse(Console.ReadLine()!);
                        
                        Console.Write("Flats per floor: ");
                        var fpf = int.Parse(Console.ReadLine()!);
                        
                        _housingEstates[_housingIndex].Add(new BlockOfFlats(street, en, bofn, fl, fpf, ref lel));
                    }
                    
                    break;
                
                case "switch":
                    if (overloads.Length != 2)
                        break;

                    foreach (var block in _housingEstates[_housingIndex].BlockOfFlats
                        .Where(block => block.BlockOfFlatsNumber == int.Parse(overloads[1])))
                        _editing += block.BlockOfFlatsNumber + ">";
                    
                    break;
                
                case "remove":
                    if (overloads.Length != 2)
                        break;
                    
                    _housingEstates[_housingIndex].Remove(int.Parse(overloads[1]));
                    
                    break;
                
                case "save":
                    break;
                
                case "name":
                    Console.WriteLine(_housingEstates[_housingIndex].Name);
                    
                    break;
                
                case "resident":
                    if (overloads.Length != 2)
                        break;

                    foreach (var resident in _housingEstates[_housingIndex].GetHousingResidents()
                        .Where(resident => resident.GetFullName() == overloads[1]))
                        resident.ShowInfo();
                    
                    break;
            }
        }

        private static void Block(string[] overloads)
        {
            switch (overloads[0])
            {
                case "create":
                    _housingEstates[_housingIndex].BlockOfFlats[_blockOfFlatsIndex].Add();

                    break;
                
                case "switch":
                    if (overloads.Length != 2)
                        break;

                    foreach (var entrance in _housingEstates[_housingIndex].BlockOfFlats[_blockOfFlatsIndex].GetEntrances()
                        .Where(entrance => entrance.EntranceNumber == int.Parse(overloads[1])))
                        _editing += entrance.EntranceNumber + ">";

                    break;
                
                case "number":
                    if(overloads.Length == 2)
                        _housingEstates[_housingIndex].BlockOfFlats[_blockOfFlatsIndex].BlockOfFlatsNumber = int.Parse(overloads[1]);

                    else
                        Console.WriteLine(_housingEstates[_housingIndex].BlockOfFlats[_blockOfFlatsIndex].BlockOfFlatsNumber);
                    
                    break;
                
                case "street":
                    if(overloads.Length == 2)
                        _housingEstates[_housingIndex].BlockOfFlats[_blockOfFlatsIndex].Street = overloads[1];

                    else
                        Console.WriteLine(_housingEstates[_housingIndex].BlockOfFlats[_blockOfFlatsIndex].Street);

                    break;
                
                case "floors":
                    if(overloads.Length == 2)
                        _housingEstates[_housingIndex].BlockOfFlats[_blockOfFlatsIndex].Floors = int.Parse(overloads[1]);

                    else
                        Console.WriteLine(_housingEstates[_housingIndex].BlockOfFlats[_blockOfFlatsIndex].Floors);

                    break;
                
                case "flats_per_floors":
                    Console.WriteLine(_housingEstates[_housingIndex].BlockOfFlats[_blockOfFlatsIndex].GetFlatsPerFloor());
                    
                    break;
            }
        }

        private static void Entrance(string[] overloads)
        {
            switch (overloads[0])
            {
                case "switch":
                    if (overloads.Length != 2)
                        break;

                    foreach (var flat in _housingEstates[_housingIndex].BlockOfFlats[_blockOfFlatsIndex].GetEntrances()[_entranceIndex].Flats
                        .Where(flat => flat.FlatNumber == int.Parse(overloads[1])))
                        _editing += flat.FlatNumber + ">";
                    
                    break;
                
                case "number":
                    Console.WriteLine(_housingEstates[_housingIndex].BlockOfFlats[_blockOfFlatsIndex].GetEntrances()[_entranceIndex].EntranceNumber);
                    
                    break;
            }
        }

        private static void Flat(string[] overloads)
        {
            switch (overloads[0])
            {
                case "create":
                    var lel =
                         _housingEstates[_housingIndex].BlockOfFlats[_blockOfFlatsIndex].GetEntrances()[
                            _entranceIndex].Flats[_flatIndex];

                    if (overloads.Length == 4)
                        _housingEstates[_housingIndex].BlockOfFlats[_blockOfFlatsIndex].GetEntrances()[
                            _entranceIndex].Flats[_flatIndex].AddResident(new Person(overloads[1], overloads[2], int.Parse(overloads[3]), ref lel));

                    else
                    {
                        Console.Write("First name: ");
                        var fName = Console.ReadLine();

                        Console.Write("Last name: ");
                        var lName = Console.ReadLine();

                        Console.Write("Age: ");
                        var age = int.Parse(Console.ReadLine()!);
                        
                        _housingEstates[_housingIndex].BlockOfFlats[_blockOfFlatsIndex].GetEntrances()[_entranceIndex].Flats[_flatIndex].AddResident(new Person(fName, lName, age, ref lel));
                    }
                    
                    break;
                
                
                case "switch": //Format of name is firstName_secondName 
                    if (overloads.Length != 2)
                        break;

                    foreach (var resident in _housingEstates[_housingIndex].BlockOfFlats[_blockOfFlatsIndex]
                        .GetEntrances()[_entranceIndex].Flats[_flatIndex].Residents
                        .Where(resident => resident.GetFullName() == overloads[1]))
                        _editing += resident.GetFullName() + ">";

                    break;
                
                case "remove":
                    if (overloads.Length != 2)
                        break;
                    
                    _housingEstates[_housingIndex].BlockOfFlats[_blockOfFlatsIndex].GetEntrances()[_entranceIndex].Flats[_flatIndex].RemoveResident(overloads[1]);
                    
                    break;
                
                case "number":
                    Console.WriteLine(_housingEstates[_housingIndex].BlockOfFlats[_blockOfFlatsIndex].GetEntrances()[_entranceIndex].Flats[_flatIndex].FlatNumber);
                    
                    break;
                
                case "floor":
                    Console.WriteLine(_housingEstates[_housingIndex].BlockOfFlats[_blockOfFlatsIndex].GetEntrances()[_entranceIndex].Flats[_flatIndex].FlatFloor);
                    
                    break;
                
            }
        }

        private static void Person(string[] overloads)
        {
            switch (overloads[0])
            {
                case "first_name":
                    Console.WriteLine(_housingEstates[_housingIndex].BlockOfFlats[_blockOfFlatsIndex].GetEntrances()[_entranceIndex].Flats[_flatIndex].Residents[_personIndex].FirstName);
                    
                    break;
                
                case "second_name":
                    Console.WriteLine(_housingEstates[_housingIndex].BlockOfFlats[_blockOfFlatsIndex].GetEntrances()[_entranceIndex].Flats[_flatIndex].Residents[_personIndex].SecondName);
                    
                    break;
                
                case "full_name":
                case "name":
                    Console.WriteLine(_housingEstates[_housingIndex].BlockOfFlats[_blockOfFlatsIndex].GetEntrances()[_entranceIndex].Flats[_flatIndex].Residents[_personIndex].GetFullName());
                    
                    break;
                
                case "age":
                    if(overloads.Length == 2)
                        _housingEstates[_housingIndex].BlockOfFlats[_blockOfFlatsIndex].GetEntrances()[_entranceIndex].Flats[_flatIndex].Residents[_personIndex].Age = int.Parse(overloads[1]);
                    
                    else
                        Console.WriteLine(_housingEstates[_housingIndex].BlockOfFlats[_blockOfFlatsIndex].GetEntrances()[_entranceIndex].Flats[_flatIndex].Residents[_personIndex].Age);

                    break;
            }
        }

        private static int UpdateEdited()
        {
            var buffer = _editing;
            return buffer.Split(">").Length - 2;
        }

        private static void Main()
        {
            _running = true;
            _editing = ">";
            _housingEstates = new List<HousingEstate>();
            _entranceIndex = 0;
            _flatIndex = 0;

            var edited = 0;

            Console.WriteLine("Housing Estate  console. For help type help.");

            while (_running)
            {
                Console.Write(_editing);
                
                var input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                    continue;

                var splitInput = input.Split(" ");

                UpdateIndexes();

                switch (splitInput[0])
                {
                    case var x when x.Contains("help"):
                        Help(splitInput);

                        break;

                    case "clear":
                    case "cls":
                        Console.Clear();

                        break;

                    case "exit":
                        if (_editing.Length > 1)    
                        {
                            var buffer = new string(_editing.Substring(0, _editing.Length - 2));
                            var splitBuffer = buffer.Split('>');
                            buffer = "";
                            
                            for (var i = 0; i < splitBuffer.Length - 1; i++)
                            {
                                buffer += splitBuffer[i] + ">";
                            }

                            _editing = buffer;
                        }
                            
                        else
                            _running = false;

                        break;

                    default:
                        UpdateIndexes();

                        switch (edited)
                        {
                            case 0:
                                Default(splitInput);//done loading, second time it just doesn't create housing : when first time write create house it will create new housingEstate but when user writes it second time IN ROW it doesn't do shit :(

                                break;

                            case 1:
                                Housing(splitInput);//done better user input, saving

                                break;

                            case 2:
                                Block(splitInput);//done

                                break;

                            case 3:
                                Entrance(splitInput);//done

                                break;

                            case 4:
                                Flat(splitInput);//done

                                break;

                            case 5:
                                Person(splitInput);//done

                                break;
                        }

                        break;
                }
                
                edited = UpdateEdited();
            }
        }
    }
}