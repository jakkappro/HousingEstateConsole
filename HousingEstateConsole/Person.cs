using System;
using System.Collections.Generic;
using System.Text;

namespace HousingEstateConsole
{
    public class Person
    {
        protected string _firstName;
        protected string _secondName;
        protected int _age;
        
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

        protected Person(string firstName, string secondName, int age)
        {
            _firstName = firstName;
            _secondName = secondName;
            _age = age;
        }
    }
}
