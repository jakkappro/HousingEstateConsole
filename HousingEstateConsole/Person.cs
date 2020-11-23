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

        public string FirstName
        {
            get => _firstName;
            set => _firstName = value;
        }

        public string SecondName
        {
            get => _secondName;
            set => _secondName = value;
        }

        public int Age
        {
            get => _age;
            set => _age = value;
        }

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

        public string ShowInfo()
        {
            var buffer = $"I am {this.FirstName} and I am living on {_flat.FlatFloor} in flat number {_flat.FlatNumber}.\n" + 
            $"My flat is in {_flat.Entrance.EntranceNumber} entrance.\n" + 
            $"My entrance is in block of flats with number {_flat.Entrance._blockOfFlats.BlockOfFlatsNumber} on {_flat.Entrance._blockOfFlats.Street} street.\n" + 
            $"This block of flats belong to {_flat.Entrance._blockOfFlats._housingEstate.Name} housing estate and there is {_flat.Entrance._blockOfFlats._housingEstate.GetHousingResidents().Count} people here.";
            return buffer;
        }
    }
}
