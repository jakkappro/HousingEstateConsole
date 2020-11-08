using System;
using System.Collections.Generic;
using System.Text;

namespace HousingEstateConsole
{
    class Person
    {
        private string _firstName;
        private string _secondName;
        private int _age;

        public Person(string firstName, string secondName, int age)
        {
            _firstName = firstName;
            _secondName = secondName;
            _age = age;
        }

        public string GetName()
        {
            return _firstName + " " + _secondName;
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
    }
}
