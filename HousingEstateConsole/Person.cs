namespace HousingEstateConsole
{
    public class Person
    {
        protected string _firstName;
        protected string _secondName;

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

        public int Age { get; set; }

        protected Person(string firstName, string secondName, int age)
        {
            _firstName = firstName;
            _secondName = secondName;
            Age = age;
        }

        protected Person()
        {
            
        }
    }
}
