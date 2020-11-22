using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        public BlockOfFlats _blockOfFlats;

        public Entrance(ref BlockOfFlats blockOfFlats) : this(0, 0, 0, ref blockOfFlats)
        {
            
        }
        public Entrance(int entranceNumber, int floors, int flatsPerFloor, ref BlockOfFlats blockOfFlats)
        {
            _flats = new List<Flat>();
            _flatNumber = 1;
            _entranceNumber = entranceNumber;
            _floors = floors;
            _flatsPerFloor = flatsPerFloor;
            _flatNumber = 0;
            _blockOfFlats = blockOfFlats;

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

        public List<Person> GetEntranceResidents()
        {
            var buffer = new List<Person>();
            foreach (var flat in _flats)
            {
                buffer.AddRange(flat.GetResidents());
            }

            return buffer;
        }

        private void CreateFlats(int oldFloors, int newFloors)
        {
            var lel = this;
            if (_flats.Count <= 0)
            {
                for (var floor = 0; floor < _floors; floor++)
                {
                    for (var flatNumber = 0; flatNumber < _flatsPerFloor; flatNumber++)
                    {
                        _flats.Add(new Flat(_flatNumber, floor, ref lel));
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
                        _flats.Add(new Flat(_flatNumber, floor, ref lel));
                        _flatNumber++;
                    }
                }
                
            }
        }
    }
}
