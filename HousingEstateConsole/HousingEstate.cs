using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HousingEstateConsole
{
    internal class HousingEstate
    {
        private string _name;
        private List<BlockOfFlats> _blockOfFlatses;
        public HousingEstate(string name)
        {
            _blockOfFlatses = new List<BlockOfFlats>();
            _name = name;
        }

        public void Add(BlockOfFlats blockOfFlats)
        {
            _blockOfFlatses.Add(blockOfFlats);
        }

        public string GetName()
        {
            return _name;
        }

        public void Remove(int number)
        {
            foreach (var blockOfFlatses in _blockOfFlatses.Where(blockOfFlatses => blockOfFlatses.GetBlockNumber() == number))
            {
                _blockOfFlatses.Remove(blockOfFlatses);
            }
        }

        public void Show()
        {
            Console.WriteLine($"Name of this housing estate is {_name}. It contains {_blockOfFlatses.Count} blocks of flats.");
        }

        public void Save()
        {
            //TODO:
        }

        public List<BlockOfFlats> GetEntrances()
        {
            return _blockOfFlatses;
        }
    }
}
