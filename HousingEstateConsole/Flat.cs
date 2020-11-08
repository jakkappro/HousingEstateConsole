using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HousingEstateConsole
{
    internal class Flat
    {
        private readonly int _flatNumber;
        private List<Person> _residents;

        public Flat(int flatNumber)
        {
            _flatNumber = flatNumber;
        }

        public Flat(int flatNumber, Person resident)
        {
            _flatNumber = flatNumber;
            _residents = new List<Person>() {resident};
        }

        public Flat(int flatNumber, IEnumerable<Person> residents)
        {
            _flatNumber = flatNumber;
            _residents = residents.ToList();
        }

        public void AddResident(Person resident)
        {
            _residents.Add(resident);
        }

        public int GetNumber()
        {
            return _flatNumber;
        }

        public void AddResidents(IEnumerable<Person> residents)
        {
            foreach (var resident in residents)
            {
                _residents.Add(resident);
            }
        }

        public void RemoveResident(string name)
        {
            var buffer = _residents.Where(resident => resident.GetName() != name).ToList();
            _residents.Clear();
            _residents = buffer;
        }

        public void Show()
        {
            Console.WriteLine("Flat number : " + _flatNumber);
        }
    }
}