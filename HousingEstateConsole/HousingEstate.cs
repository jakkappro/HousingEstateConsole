using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HousingEstateConsole
{
    public class HousingEstate : IShowable
    {
        public string Name { get; set; }

        public List<BlockOfFlats> BlockOfFlats { get; private set; }

        public HousingEstate(string name)
        {
            BlockOfFlats = new List<BlockOfFlats>();
            Name = name;
        }
        
        public List<Resident> GetHousingResidents()
        {
            var buffer = new List<Resident>();

            foreach (var block in BlockOfFlats)
            {
                buffer.AddRange(block.GetBlockResidents());
            }

            return buffer;
        }

        public void Add(List<object> variables)
        {
            var block = new BlockOfFlats(variables[0] as string,
                int.Parse(variables[1] as string ?? throw new ArgumentException()),
                int.Parse(variables[2] as string ?? throw new ArgumentException()),
                int.Parse(variables[3] as string ?? throw new ArgumentException()),
                int.Parse(variables[4] as string ?? throw new ArgumentException()), this);
            BlockOfFlats.Add(block);
        }

        public void Show()
        {
            
        }

        public void Change(string what, string to)
        {
            if (what == "name")
                Name = to;
        }

        public IShowable GetParent()
        {
            return null;
        }

        public string GetWriteName()
        {
            return Name;
        }

        public string GetStructure()
        {
            return BlockOfFlats.Aggregate("", (current, block) => current + block.BlockOfFlatsNumber.ToString() + "\n");
        }
    }
}