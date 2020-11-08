using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HousingEstateConsole
{
    internal class BlockOfFlats
    {
        private List<Entrance> _entrances;
        private readonly string _street;
        private int _entranceNumber;
        private readonly int _blockOfFlatsNumber;
        
        public BlockOfFlats(string street, int entranceNumber, int blockOfFlatsNumber)
        {
            _street = street;
            _entranceNumber = entranceNumber;
            _blockOfFlatsNumber = blockOfFlatsNumber;
            _entrances = new List<Entrance>();
        }
        
        public void Add(Entrance entrance)
        {
            entrance.SetNumber(_entranceNumber);
            _entrances.Add(entrance);
            _entranceNumber += 2;
        }

        public void Add(IEnumerable<Entrance> entrances)
        {
            foreach (var entrance in entrances)
            {
                entrance.SetNumber(_entranceNumber);
                _entrances.Add(entrance);
                _entranceNumber += 2;
            }
        }

        public void Remove(int number)
        {
            var buffer = _entrances.Where(entrance => entrance.GetNumber() != number).ToList();
            _entrances.Clear();
            _entrances = buffer;
            //TODO: Fix removing now it remove entrance but doesnt change number of other entrances
        }

        public Entrance GetEntrance(int number)
        {
            foreach (var entrance in _entrances.Where(entrance => entrance.GetNumber() == number))
            {
                return entrance;
            }
            
            throw new ArgumentException($"Entrance with number {number} does not exist.");
        }
        
        public List<Entrance> GetEntrances()
        {
            return _entrances;
        }

        public string GetStreet()
        {
            return _street;
        }

        public int GetBlockNumber()
        {
            return _blockOfFlatsNumber;
        }
        
        public void Show()
        {
            Console.WriteLine("Street : " + _street +
                              "Block of flats number : " + _blockOfFlatsNumber+
                              "Number of entrances :" + _entrances.Count
                              );
        }
    }
}
