using System;
using System.Collections.Generic;
using System.Text;

namespace HousingEstateConsole
{
    internal class Entrance
    {
        private int _entranceNumber;
        private int _floors;
        private int _flatsPerFloor;
        private int _flatNumber;
        private Dictionary<int, List<Flat>> _flats;


        public Entrance() : this(0, 0, 0)
        {
            
        }
        public Entrance(int entranceNumber, int floors, int flatsPerFloor)
        {
            _flats = new Dictionary<int, List<Flat>>();
            _flatNumber = 1;
            _entranceNumber = entranceNumber;
            _floors = floors;
            _flatsPerFloor = flatsPerFloor;
            _flatNumber = 0;

            for (var y = 0; y < _floors; y++)
            {
                var flats = new List<Flat>();
                for (var i = 0; i < _flatsPerFloor; i++)
                {
                    flats.Add(new Flat(_flatNumber));
                    _flatNumber += 1;
                }
                _flats.Add(y, flats);
            }
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

        public int GetFloors()
        {
            return _floors;
        }

        public void SetFloors(int floors)
        {
            _floors = floors;
            //TODO:recalculate and add flats with numbers
        }

        public void AddFloors(int floors)
        {
            _floors += floors;
            //TODO:recalculate and add flats with numbers
        }

        public int GetFlatsPerFloor()
        {
            return _flatsPerFloor;
        }

        public void SetFlatsPerFloor(int flats)
        {
            _flatsPerFloor += flats;
            //TODO:recalculate and add flats with numbers
        }

        public int GetNumberOfHouses()
        {
            return _floors * _flatsPerFloor;
        }

        public Dictionary<int, List<Flat>> GetFlats()
        {
            return _flats;
        }
    }
}
