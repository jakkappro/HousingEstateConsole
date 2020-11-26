using System.Collections.Generic;

namespace HousingEstateConsole
{
    public interface IShowable
    {
        public void Add(List<object> variables);

        public string Show();

        public void Change(string what, string to);

        public IShowable GetParent();

        public string GetWriteName();

        public string GetStructure();
    }
}