using System;
using System.Collections.Generic;
using System.Text;

namespace HousingEstateConsole
{
    class Person
    {
        private Flat _flat;
        
        private string _firstName;
        private string _secondName;
        private int _age;

        public Person(string firstName, string secondName, int age, ref Flat flat)
        {
            _firstName = firstName;
            _secondName = secondName;
            _age = age;

            _flat = flat;
        }

        public string GetFullName()
        {
            return _firstName + "_" + _secondName;
        }

        public string GetFirstName()
        {
            return _firstName;
        }

        public string GetSecondName()
        {
            return _secondName;
        }

        public int GetAge()
        {
            return _age;
        }

        public void SetAge(int age)
        {
            _age = age;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"I am {this.GetFirstName()} and I am living on {_flat.GetFloor()} in flat number {_flat.GetNumber()}.");
            Console.WriteLine($"My flat is in {_flat._entrance.GetNumber()} entrance.");
            Console.WriteLine($"My entrance is in block of flats with number {_flat._entrance._blockOfFlats.GetBlockNumber()} on {_flat._entrance._blockOfFlats.GetStreet()} street");
            Console.WriteLine($"This block of flats belong to {_flat._entrance._blockOfFlats._housingEstate.GetName()} housing estate and there is {_flat._entrance._blockOfFlats._housingEstate.GetHousingResidents().Count} people here.");
        }
    }
}
