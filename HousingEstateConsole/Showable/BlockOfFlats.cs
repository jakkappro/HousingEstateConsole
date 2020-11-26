using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace HousingEstateConsole
{
    
    public class BlockOfFlats : IShowable
    {
        public int area;
        public int rooms;
        public int _entranceNumber;
        public int _floors;
        public readonly int _flatsPerFloor;

        [XmlIgnore]
        public HousingEstate _housingEstate;

        public List<Entrance> Entrances { get; }

        public string Street { get; set; }

        public int BlockOfFlatsNumber { get; set; }

        public int Floors
        {
            get => _floors;
            set
            {
                if(_floors != 0)
                {
                    foreach (var entrance in Entrances)
                        entrance.ChangeFloors(Floors);
                }
                
                _floors = value;
            }
        }

        public BlockOfFlats(string street, int entranceNumber, int blockOfFlatsNumber, int floors, int flatsPerFloor, int area, int rooms, HousingEstate housingEstate)
        {
            Floors = floors;
            _flatsPerFloor = flatsPerFloor;
            Street = street;
            _entranceNumber = entranceNumber;
            BlockOfFlatsNumber = blockOfFlatsNumber;
            Entrances = new List<Entrance>();
            _housingEstate = housingEstate;
            this.area = area;
            this.rooms = rooms;
        }
        
        public List<Resident> GetBlockResidents()
        {
            var buffer = new List<Resident>();
            
            foreach(var entrance in Entrances)
                buffer.AddRange(entrance.GetEntranceResidents());

            return buffer;
        }
        
        //TODO:add stuffs like upratovacka ...

        public void Add(List<object> variables)
        {
            var entrance = new Entrance(_entranceNumber, _floors, _flatsPerFloor, area, rooms,this);
            
            Entrances.Add(entrance);
            _entranceNumber += 2;
        }

        public string Show()
        {
            return $"This is block of flats with number {BlockOfFlatsNumber}. It has {GetBlockResidents().Count} residents in {Entrances.Count} entrances. This block of flats is on street {Street} and has {Floors} floors.";
        }

        public void Change(string what, string to)
        {
            switch (what)
            {
                case "street":
                    Street = to;
                    break;
                
                case "number":
                    BlockOfFlatsNumber = int.Parse(to);
                    break;
            }
        }

        public IShowable GetParent()
        {
            return _housingEstate;
        }

        public string GetWriteName()
        {
            return BlockOfFlatsNumber.ToString();
        }

        public string GetStructure()
        {
            return Entrances.Aggregate("", (current, entrance) => current + (entrance.EntranceNumber + "\n"));
        }

        public BlockOfFlats()
        {

        }
    }
}
