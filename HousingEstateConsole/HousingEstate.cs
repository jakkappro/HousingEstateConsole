using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HousingEstateConsole
{
    public class HousingEstate
    {
        public string Name { get; }

        public List<BlockOfFlats> BlockOfFlats { get; private set; }

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

        public void Save(string path)
        {
            path += "\\" + Name + ".txt";
            if (File.Exists(path))
                File.Delete(path);

            using var sw = File.CreateText(path);
            
            sw.Write(@"\" + "\n" + "name: " +  Name + "\n");
            sw.Write("\n;\n");
            
            foreach (var block in BlockOfFlats)
            {
                sw.Write(@"\\" + "\n");
                sw.Write(block.GetData());
                sw.Write("\n;\n");

                foreach (var entrance in block.Entrances)
                {
                    sw.Write(@"\\\" + "\n");
                    sw.Write($"entranceNumber: {entrance.EntranceNumber}");
                    sw.Write("\n;\n");
                    
                    foreach (var flat in entrance.Flats)
                    {
                        sw.Write(@"\\\\" + "\n");
                        sw.Write(flat.GetData());
                        sw.Write("\n;\n");

                        foreach (var person in flat.Residents)
                        {
                            sw.Write(@"\\\\\" + "\n" + person.GetFullName() + "\n");
                            sw.Write(person.GetData());
                            sw.Write("\n;\n");
                        }
                    }
                }
            }
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
    }
}
