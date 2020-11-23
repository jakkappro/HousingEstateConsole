using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HousingEstateConsole
{
    internal class Flat
    {
        public readonly Entrance Entrance;

        public List<Person> Residents { get; private set; }

        public int FlatNumber { get; }

        public int FlatFloor { get; }

        public Flat(int flatNumber, int flatFloor, ref Entrance entrance)
        {
            FlatNumber = flatNumber;
            FlatFloor = flatFloor;
            Residents = new List<Person>();
            Entrance = entrance;
        }

        public void AddResident(Person resident)
        {
            Residents.Add(resident);
        }
        
        public void RemoveResident(string name)
        {
            var buffer = Residents.Where(resident => resident.GetFullName() != name).ToList();
            Residents.Clear();
            Residents = buffer;
        }
    }
}