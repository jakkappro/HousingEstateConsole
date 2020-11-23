using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HousingEstateConsole
{
    internal class HousingEstate
    {
        public string Name { get; }

        public List<BlockOfFlats> BlockOfFlats { get; }

        public HousingEstate(string name)
        {
            BlockOfFlats = new List<BlockOfFlats>();
            Name = name;
        }

        public void Add(BlockOfFlats blockOfFlats)
        {
            BlockOfFlats.Add(blockOfFlats);
        }
        
        public void Remove(int number)
        {
            foreach (var blockOfFlatses in BlockOfFlats.Where(blockOfFlatses => blockOfFlatses.BlockOfFlatsNumber == number))
            {
                BlockOfFlats.Remove(blockOfFlatses);
            }
        }

        public void Save()
        {
            //TODO:
        }

        public List<Person> GetHousingResidents()
        {
            var buffer = new List<Person>();

            foreach (var block in BlockOfFlats)
            {
                buffer.AddRange(block.GetBlockResidents());
            }

            return buffer;
        }
    }
}
