using System.Collections.Generic;

namespace HousingEstateConsole
{
    public class BlockOfFlats
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
        
        public void Add()
        {
            var lel = this;
            var entrance = new Entrance(_entranceNumber, Floors, _flatsPerFloor, lel);
            Entrances.Add(entrance);
            _entranceNumber += 2;
        }

        public int GetFlatsPerFloor()
        {
            return _flatsPerFloor;
        }

        public List<Resident> GetBlockResidents()
        {
            var buffer = new List<Resident>();
            
            foreach(var entrance in Entrances)
                buffer.AddRange(entrance.GetEntranceResidents());

            return buffer;
        }
        
        //TODO:add stuffs like upratovacka ...
        public string GetData()
        {
            return $"street: {Street}\nentranceNumber: {_entranceNumber}\nblockOfFlatsNumber: {BlockOfFlatsNumber}\nfloors: {_floors}\nflatsPerFloor: {_flatsPerFloor}\n";
        }
    }
}
