using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace HousingEstateConsole
{
    internal class Entrance
    {
        private int _entranceNumber;
        private int _floors;
        private readonly int _flatsPerFloor;
        private int _flatNumber;
        private List<Flat> _flats;

        public Entrance() : this(0, 0, 0)
        {
            
        }
        public Entrance(int entranceNumber, int floors, int flatsPerFloor)
        {
            _flats = new List<Flat>();
            _flatNumber = 1;
            _entranceNumber = entranceNumber;
            _floors = floors;
            _flatsPerFloor = flatsPerFloor;
            _flatNumber = 0;

            CreateFlats(floors,floors);
        }

        public void ChangeFloors(int floors = 0)
        {
            CreateFlats(_floors, floors);
            _floors = floors;
        }

        public int GetNumber()
        {
            return _entranceNumber;
        }

        public void SetNumber(int number)
        {
            if(number < 0)
                throw new ArgumentException("Entrance number can not be lower than 0.");
            
            _entranceNumber = number;
        }

        public List<Flat> GetFlats()
        {
            return _flats;
        }

        private void CreateFlats(int oldFloors, int newFloors)
        {
            if (_flats.Count <= 0)
            {
                for (var floor = 0; floor < _floors; floor++)
                {
                    for (var flatNumber = 0; flatNumber < _flatsPerFloor; flatNumber++)
                    {
                        _flats.Add(new Flat(_flatNumber, floor));
                        _flatNumber++;
                    }
                }
            }
            
            else
            {
                _flatNumber = _flats[^1].GetNumber();
                for (var floor = oldFloors; floor < newFloors; floor++)
                {
                    for (var flatNumber = 0; flatNumber < _flatsPerFloor; flatNumber++)
                    {
                        _flats.Add(new Flat(_flatNumber, floor));
                        _flatNumber++;
                    }
                }
                
            }
        }
    }
}
