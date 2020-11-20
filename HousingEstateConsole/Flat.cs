using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HousingEstateConsole
{
    internal class Flat
    {
        private readonly int _flatNumber;
        private readonly int _flatFloor;
        private List<Person> _residents;

        public Flat(int flatNumber, int flatFloor)
        {
            _flatNumber = flatNumber;
            _flatFloor = flatFloor;
            _residents = new List<Person>();
        }

        public int GetNumber()
        {
            return _flatNumber;
        }

        public int GetFloor()
        {
            return _flatFloor;
        }

        public void AddResident(Person resident)
        {
            _residents.Add(resident);
        }
        
        public void RemoveResident(string name)
        {
            var buffer = _residents.Where(resident => resident.GetFullName() != name).ToList();
            _residents.Clear();
            _residents = buffer;
        }

        public List<Person> GetResidents()
        {
            return _residents;
        }
    }
}