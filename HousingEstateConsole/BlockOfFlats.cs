using System;
using System.Collections.Generic;

namespace HousingEstateConsole
{
    public class BlockOfFlats : IShowable
    {
        private int _entranceNumber;
        private int _floors;
        private readonly int _flatsPerFloor;
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

        public BlockOfFlats(string street, int entranceNumber, int blockOfFlatsNumber, int floors, int flatsPerFloor, HousingEstate housingEstate)
        {
            Floors = floors;
            _flatsPerFloor = flatsPerFloor;
            Street = street;
            _entranceNumber = entranceNumber;
            BlockOfFlatsNumber = blockOfFlatsNumber;
            Entrances = new List<Entrance>();
            _housingEstate = housingEstate;
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
            var entrance = new Entrance(_entranceNumber, _floors, _flatsPerFloor, this);
            
            Entrances.Add(entrance);
            _entranceNumber += 2;
        }

        public void Show()
        {
            
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
            var buffer = "";

            foreach (var entrance in Entrances)
            {
                buffer += entrance.EntranceNumber + "\n";
            }

            return buffer;
        }
    }
}
