using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace HousingEstateConsole
{
    public class Flat : IShowable
    {
        [XmlIgnore]
        public readonly Entrance Entrance;

        public List<Resident> Residents { get; }

        public int FlatNumber { get; }

        public int FlatFloor { get; }

        public int FlatArea { get; set; }

        public int FlatRooms { get; set; }

        public Flat(int flatNumber, int flatFloor, int flatArea, int flatRooms, Entrance entrance)
        {
            FlatNumber = flatNumber;
            FlatFloor = flatFloor;
            Residents = new List<Resident>();
            Entrance = entrance;
            FlatArea = flatArea;
            FlatRooms = flatRooms;
        }

        public void Add(List<object> variables)
        {
            var resident = new Resident(variables[0] as string, variables[1] as string,
                int.Parse(variables[2] as string ?? throw new ArgumentException()), this);
            Residents.Add(resident);
        }

        public string Show()
        {
            return
                $"This is flat with number {FlatNumber}. This flat is on {FlatFloor} and has {FlatRooms} rooms. {Residents.Count} residents live here.";
        }

        public void Change(string what, string to)
        {
            switch (what)
            {
                case "area":
                    FlatArea = int.Parse(to);

                    break;

                case "rooms":
                    FlatRooms = int.Parse(to);

                    break;
            }
        }

        public IShowable GetParent()
        {
            return Entrance;
        }

        public string GetWriteName()
        {
            return FlatNumber.ToString();
        }

        public string GetStructure()
        {
            return Residents.Aggregate("", (current, resident) => current + (resident.GetWriteName() + "\n"));
        }

        public Flat()
        {

        }
    }
}