using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HousingEstateConsole
{
    internal class BlockOfFlats
    {
        private List<Entrance> _entrances;
        private string _street;
        private int _entranceNumber;
        private int _blockOfFlatsNumber;
        private int _floors;
        private readonly int _flatsPerFloor;
        
        public BlockOfFlats(string street, int entranceNumber, int blockOfFlatsNumber, int floors, int flatsPerFloor)
        {
            _floors = floors;
            _flatsPerFloor = flatsPerFloor;
            _street = street;
            _entranceNumber = entranceNumber;
            _blockOfFlatsNumber = blockOfFlatsNumber;
            _entrances = new List<Entrance>();
        }
        
        public void Add()
        {
            var entrance = new Entrance(_entranceNumber, _floors, _flatsPerFloor);
            _entrances.Add(entrance);
            _entranceNumber += 2;
        }

        public List<Entrance> GetEntrances()
        {
            return _entrances;
        }

        public string GetStreet()
        {
            return _street;
        }

        public void SetStreet(string street)
        {
            _street = street;
        }

        public int GetBlockNumber()
        {
            return _blockOfFlatsNumber;
        }

        public void SetBlockNumber(int number)
        {
            _blockOfFlatsNumber = number;
        }

        public int GetFlatsPerFloor()
        {
            return _flatsPerFloor;
        }

        public int GetFloors()
        {
            return _floors;
        }

        public void SetFloors(int floors)
        {
            _floors = floors;
            
            foreach(var entrance in _entrances)
                entrance.ChangeFloors(floors);
        }
    }
}
