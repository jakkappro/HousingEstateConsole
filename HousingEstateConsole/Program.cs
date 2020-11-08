using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Sockets;

namespace HousingEstateConsole
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Housing Estate console. For help type help.");

            var running = true;
            var edited = 0;
            var editing = "";
            var entranceNumber = 0;
            var housingEstates = new List<HousingEstate>();
            var entranceIndex = 0;
            var flatIndex = new int[2];


            while (running)
            {
                Console.Write(editing + ">");

                var input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                    continue;

                var splintedInput = input.Split(" ");

                switch (edited)
                {
                    case 0:

                        switch (splintedInput[0])
                        {
                            case "help":
                                break;

                            case "clear":
                                Console.Clear();
                                break;

                            case "create":
                                HousingEstate housingEstate;
                                if (splintedInput.Length > 1)
                                {
                                    housingEstate = new HousingEstate(splintedInput[1]);
                                    housingEstates.Add(housingEstate);
                                    editing = housingEstate.GetName();
                                    edited = 1;
                                    break;
                                }

                                Console.Write("Type name of housing estate: ");
                                var name = Console.ReadLine();
                                housingEstate = new HousingEstate(name);
                                editing = housingEstate.GetName();
                                edited = 1;
                                housingEstates.Add(housingEstate);
                                break;

                            case "switch":
                                if (splintedInput.Length > 1)
                                {
                                    foreach (var housing in housingEstates.Where(housing =>
                                        housing.GetName() == splintedInput[1]))
                                    {
                                        edited = 1;
                                        editing = housing.GetName();
                                    }
                                }

                                break;

                            case "load":
                                break;

                            case "quit":
                            case "exit":
                                running = false;
                                break;
                        }

                        break;

                    case 1:

                        var index = 0;

                        foreach (var housingEstate in housingEstates.Where(housingEstate =>
                            housingEstate.GetName() == editing))
                        {
                            index = housingEstates.IndexOf(housingEstate);
                        }

                        switch (splintedInput[0])
                        {
                            case "help":
                                break;

                            case "exit":
                                edited = 0;
                                editing = "";
                                break;

                            case "clear":
                                Console.Clear();
                                break;

                            case "create":
                                if (splintedInput.Length == 4)
                                    housingEstates[index].Add(new BlockOfFlats(splintedInput[1],
                                        int.Parse(splintedInput[2]),
                                        int.Parse(splintedInput[3])));

                                else
                                {
                                    Console.Write("Street :");
                                    var street = Console.ReadLine() ?? "Empty";
                                    Console.Write("Entrance number: ");
                                    var entrance = int.Parse(Console.ReadLine() ?? "0");
                                    Console.Write("Block number :");
                                    var blockNumber = int.Parse(Console.ReadLine() ?? "0");
                                    housingEstates[index].Add(new BlockOfFlats(street, entrance, blockNumber));
                                }

                                break;

                            case "remove":
                                if (splintedInput.Length > 1)
                                    housingEstates[index].Remove(int.Parse(splintedInput[1]));

                                break;

                            case "show":
                                housingEstates[index].Show();
                                break;

                            case "save":
                                break;

                            case "switch":
                                if (!(splintedInput.Length > 1))
                                    break;
                                if (housingEstates[index].GetEntrances().Any(entrance =>
                                    entrance.GetBlockNumber() == int.Parse(splintedInput[1])))
                                {
                                    editing += $">{splintedInput[1]}";
                                    entranceNumber = int.Parse(splintedInput[1]);
                                    edited = 2;
                                }

                                break;
                        }

                        break;

                    case 2:

                        var index1 = 0;

                        foreach (var housingEstate in housingEstates.Where(housingEstate =>
                            housingEstate.GetName() == editing))
                        {
                            index1 = housingEstates.IndexOf(housingEstate);
                        }

                        var blockOfFlatsIndex = 0;

                        foreach (var entrance in housingEstates[index1].GetEntrances()
                            .Where(entrance => entrance.GetBlockNumber() == entranceNumber))
                            blockOfFlatsIndex = housingEstates[index1].GetEntrances().IndexOf(entrance);

                        switch (splintedInput[0])
                        {
                            case "help":
                                break;

                            case "clear":
                                Console.Clear();
                                break;

                            case "create":
                                if (splintedInput.Length == 3)
                                {
                                    housingEstates[index1].GetEntrances()[blockOfFlatsIndex].Add(new Entrance(0,
                                        int.Parse(splintedInput[1]), int.Parse(splintedInput[2])));
                                    break;
                                }

                                Console.Write("Floors :");
                                var floors = int.Parse(Console.ReadLine());
                                Console.Write("Flats per floor :");
                                var flatsPF = int.Parse(Console.ReadLine());
                                housingEstates[index1].GetEntrances()[blockOfFlatsIndex]
                                    .Add(new Entrance(0, floors, flatsPF));

                                break;

                            case "exit":
                                edited = 1;
                                editing = editing.Substring(0, editing.Length - 2).TrimEnd('>');
                                entranceNumber = -1;
                                break;

                            case "switch":
                                if (splintedInput.Length != 2)
                                    break;

                                foreach (var entrance in housingEstates[index1].GetEntrances()[blockOfFlatsIndex]
                                    .GetEntrances().Where(entrance =>
                                        entrance.GetNumber() == int.Parse(splintedInput[1])))
                                {
                                    edited = 3;
                                    editing += $">{entrance.GetNumber()}";
                                    entranceIndex = housingEstates[index1].GetEntrances()[blockOfFlatsIndex]
                                        .GetEntrances().IndexOf(entrance);
                                }

                                break;

                            case "show":
                                housingEstates[index1].GetEntrances()[blockOfFlatsIndex].Show();
                                break;

                            case "number":
                                Console.WriteLine(housingEstates[index1].GetEntrances()[blockOfFlatsIndex]
                                    .GetBlockNumber());
                                break;

                            case "street":
                                Console.WriteLine(housingEstates[index1].GetEntrances()[blockOfFlatsIndex].GetStreet());
                                break;
                        }

                        break;

                    case 3:

                        var index2 = 0;

                        foreach (var housingEstate in housingEstates.Where(housingEstate =>
                            housingEstate.GetName() == editing))
                        {
                            index2 = housingEstates.IndexOf(housingEstate);
                        }

                        var blockOfFlatsIndex1 = 0;

                        foreach (var entrance in housingEstates[index2].GetEntrances()
                            .Where(entrance => entrance.GetBlockNumber() == entranceNumber))
                            blockOfFlatsIndex1 = housingEstates[index2].GetEntrances().IndexOf(entrance);

                        switch (splintedInput[0])
                        {
                            case "help":
                                break;

                            case "clear":
                                Console.Clear();
                                break;

                            case "switch":
                                if (splintedInput.Length != 2) //switch flatNumber
                                    break;

                                foreach (var (floor, f) in housingEstates[index2].GetEntrances()[blockOfFlatsIndex1]
                                    .GetEntrances()[entranceIndex].GetFlats())
                                {
                                    foreach (var flat in housingEstates[index2].GetEntrances()[blockOfFlatsIndex1]
                                        .GetEntrances()[entranceIndex].GetFlats()[floor].Where(flat =>
                                            flat.GetNumber() == int.Parse(splintedInput[1])))
                                    {
                                        edited = 4;
                                        editing += editing + $">{flat.GetNumber()}";
                                        flatIndex[0] = floor;
                                        flatIndex[1] =
                                            housingEstates[index2].GetEntrances()[blockOfFlatsIndex1].GetEntrances()[
                                                entranceIndex].GetFlats()[floor].IndexOf(flat);
                                    }
                                }

                                break;

                            case "getNumber":
                                Console.WriteLine(
                                    housingEstates[index2].GetEntrances()[blockOfFlatsIndex1].GetEntrances()[
                                            entranceIndex]
                                        .GetNumber());
                                break;

                            case "exit":
                                edited--;
                                editing = editing.Substring(0, editing.Length - 2).TrimEnd('>');
                                break;

                            case "getNumberOfHouses":
                                Console.WriteLine(
                                    housingEstates[index2].GetEntrances()[blockOfFlatsIndex1].GetEntrances()[
                                            entranceIndex]
                                        .GetNumberOfHouses());
                                break;

                            case "getFloors":
                                Console.WriteLine(
                                    housingEstates[index2].GetEntrances()[blockOfFlatsIndex1].GetEntrances()[
                                            entranceIndex]
                                        .GetFloors());
                                break;

                            case "setFloors":
                                if (splintedInput.Length != 2)
                                    break;

                                housingEstates[index2].GetEntrances()[blockOfFlatsIndex1].GetEntrances()[entranceIndex]
                                    .SetFloors(int.Parse(splintedInput[1]));
                                break;

                            case "addFloors":
                                if (splintedInput.Length != 2)
                                    break;

                                housingEstates[index2].GetEntrances()[blockOfFlatsIndex1].GetEntrances()[entranceIndex]
                                    .AddFloors(int.Parse(splintedInput[1]));
                                break;

                            case "getFlatsPerFloor":
                                Console.WriteLine(
                                    housingEstates[index2].GetEntrances()[blockOfFlatsIndex1].GetEntrances()[
                                            entranceIndex]
                                        .GetFlatsPerFloor());
                                break;

                            case "setFlatsPerFloor":
                                if (splintedInput.Length != 2)
                                    break;

                                housingEstates[index2].GetEntrances()[blockOfFlatsIndex1].GetEntrances()[entranceIndex]
                                    .SetFlatsPerFloor(int.Parse(splintedInput[1]));
                                break;
                        }

                        break;

                    case 4:

                        var index3 = 0;

                        foreach (var housingEstate in housingEstates.Where(housingEstate =>
                            housingEstate.GetName() == editing))
                        {
                            index3 = housingEstates.IndexOf(housingEstate);
                        }

                        var blockOfFlatsIndex2 = 0;

                        foreach (var entrance in housingEstates[index3].GetEntrances()
                            .Where(entrance => entrance.GetBlockNumber() == entranceNumber))
                            blockOfFlatsIndex2 = housingEstates[index3].GetEntrances().IndexOf(entrance);

                        switch (splintedInput[0])
                        {
                            case "help":
                                break;

                            case "clear":
                                Console.Clear();
                                break;

                            case "show":
                                housingEstates[index3].GetEntrances()[blockOfFlatsIndex2].GetEntrances()[entranceIndex]
                                    .GetFlats()[flatIndex[0]][flatIndex[1]].Show();
                                break;

                            case "exit":
                                edited--;
                                editing = editing.Substring(0, editing.Length - 2).TrimEnd('>');
                                break;

                            case "create":
                                if (splintedInput.Length != 4)
                                    break;
                                housingEstates[index3].GetEntrances()[blockOfFlatsIndex2].GetEntrances()[entranceIndex]
                                    .GetFlats()[flatIndex[0]][flatIndex[1]].AddResident(new Person(splintedInput[1],
                                        splintedInput[2], int.Parse(splintedInput[3])));  //create firstName secondName age
                                break;

                            case "remove":
                                if (splintedInput.Length != 2)
                                    break;
                                
                                housingEstates[index3].GetEntrances()[blockOfFlatsIndex2].GetEntrances()[entranceIndex]
                                    .GetFlats()[flatIndex[0]][flatIndex[1]].RemoveResident(splintedInput[1]);
                                break;

                            case "switch":
                                if (splintedInput.Length != 2)
                                    break;

                                edited = 5;
                                editing += $">{splintedInput[1]}";
                                break;
                        }

                        break;

                    case 5:
                        break;
                }
            }
        }
    }
}