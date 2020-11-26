using System.Collections.Generic;
using System.Linq;

namespace HousingEstateConsole
{
    public static class CityManager
    {
        public static IShowable ShowAble;

        public static void Exit()
        {
            ShowAble = ShowAble.GetParent();
        }

        public static bool Switch(string to)
        {
            if (!int.TryParse(to, out _) &&  ShowAble.GetType().ToString() != typeof(Flat).ToString())
                return false;
            
            switch (ShowAble)
            {
                case HousingEstate buffer:
                    foreach (var block in buffer.BlockOfFlats.Where(block => block.BlockOfFlatsNumber == int.Parse(to)))
                    {
                        ShowAble = block;
                        return true;
                    }

                    break;

                case BlockOfFlats buffer:
                    foreach (var entrance in buffer.Entrances.Where(
                        entrance => entrance.EntranceNumber == int.Parse(to)))
                    {
                        ShowAble = entrance;
                        return true;
                    }

                    break;

                case Entrance buffer:
                    foreach (var flat in buffer.Flats.Where(flat => flat.FlatNumber == int.Parse(to)))
                    {
                        ShowAble = flat;
                        return true;
                    }

                    break;

                case Flat buffer:
                    foreach (var resident in buffer.Residents.Where(resident => resident.GetFullName() == to))
                    {
                        ShowAble = resident;
                        return true;
                    }

                    break;
            }

            return false;
        }

        public static void Create(List<object> variables)
        {
            ShowAble.Add(variables);
        }

        public static void Save()
        {
        }

        public static void Load()
        {
        }

        public static void Change(string what, string to)
        {
            ShowAble.Change(what, to);
        }

        public static void Init(string name)
        {
            var housing = new HousingEstate(name);
            ShowAble = housing;
        }
    }
}