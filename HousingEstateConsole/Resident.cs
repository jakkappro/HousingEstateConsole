using System;
using System.Collections.Generic;

namespace HousingEstateConsole
{
    [Serializable]
    public class Resident : Person, IShowable
    {
        private readonly Flat _flat;
        
        public Resident(string firstName, string secondName, int age, Flat flat) : base(firstName, secondName, age)
        {
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

        public string GetData()
        {
            return $"firstName: {FirstName}\nsecondName: {SecondName}\nage: {Age}";
        }

        public void Add(List<object> variables)
        {
            
        }

        public void Show()
        {
            
        }

        public void Change(string what, string to)
        {
            switch (what)
            {
                case "firstName":
                    FirstName = to;
                    break;
                
                case "secondName":
                    SecondName = to;
                    break;
                
                case "age":
                    Age = int.Parse(to);
                    break;
            }
        }

        public IShowable GetParent()
        {
            return _flat;
        }

        public string GetWriteName()
        {
            return GetFullName();
        }

        public string GetStructure()
        {
            throw new NotImplementedException();
        }
    }
}