using System;
using System.Collections.Generic;
using System.Linq;

namespace HousingEstateConsole
{
    public static class CityManager
    {
        public static IShowable _showAble;

        public static void Exit()
        {
            _showAble = _showAble.GetParent();
        }

        public static bool Switch(string to)
        {
            if (!int.TryParse(to, out _) && _showAble.GetType() != typeof(Flat))
                return false;
            
            switch (_showAble)
            {
                case HousingEstate buffer:
                    foreach (var block in buffer.BlockOfFlats.Where(block => block.BlockOfFlatsNumber == int.Parse(to)))
                    {
                        _showAble = block;
                        return true;
                    }

                    break;

                case BlockOfFlats buffer:
                    foreach (var entrance in buffer.Entrances.Where(
                        entrance => entrance.EntranceNumber == int.Parse(to)))
                    {
                        _showAble = entrance;
                        return true;
                    }

                    break;

                case Entrance buffer:
                    foreach (var flat in buffer.Flats.Where(flat => flat.FlatNumber == int.Parse(to)))
                    {
                        _showAble = flat;
                        return true;
                    }

                    break;

                case Flat buffer:
                    foreach (var resident in buffer.Residents.Where(resident => resident.GetFullName() == to))
                    {
                        _showAble = resident;
                        return true;
                    }

                    break;
            }

            return false;
        }

        public static void Create(List<object> variables)
        {
            _showAble.Add(variables);
        }

        public static void Save()
        {
        }

        public static void Load()
        {
        }

        public static void Change(string what, string to)
        {
            _showAble.Change(what, to);
        }

        public static void Init(string name)
        {
            var housing = new HousingEstate(name);
            _showAble = housing;
        }
    }
}