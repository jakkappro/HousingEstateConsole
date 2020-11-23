using System.Collections.Generic;

namespace HousingEstateConsole
{
    internal class BlockOfFlats
    {
        private List<Entrance> _entrances;
        private int _entranceNumber;
        private int _floors;
        private readonly int _flatsPerFloor;
        public HousingEstate _housingEstate;

        public string Street { get; set; }

        public int BlockOfFlatsNumber { get; set; }

        public int Floors
        {
            get => _floors;
            set
            {
                _floors = value;
            
                foreach(var entrance in _entrances)
                    entrance.ChangeFloors(Floors);
            }
        }

        public BlockOfFlats(string street, int entranceNumber, int blockOfFlatsNumber, int floors, int flatsPerFloor, ref HousingEstate housingEstate)
        {
            Floors = floors;
            _flatsPerFloor = flatsPerFloor;
            Street = street;
            _entranceNumber = entranceNumber;
            BlockOfFlatsNumber = blockOfFlatsNumber;
            _entrances = new List<Entrance>();
            _housingEstate = housingEstate;
        }
        
        public void Add()
        {
            var lel = this;
            var entrance = new Entrance(_entranceNumber, Floors, _flatsPerFloor, ref lel);
            _entrances.Add(entrance);
            _entranceNumber += 2;
        }

        public List<Entrance> GetEntrances()
        {
            return _entrances;
        }

        public int GetFlatsPerFloor()
        {
            return _flatsPerFloor;
        }

        public List<Person> GetBlockResidents()
        {
            var buffer = new List<Person>();
            
            foreach(var entrance in _entrances)
                buffer.AddRange(entrance.GetEntranceResidents());

            return buffer;
        }
        
        //TODO:add stuffs like upratovacka ...
    }
}
