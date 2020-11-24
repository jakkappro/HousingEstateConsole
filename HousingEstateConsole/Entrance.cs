using System;
using System.Collections.Generic;

namespace HousingEstateConsole
{
    public class Entrance
    {
        private int _floors;
        private readonly int _flatsPerFloor;
        private int _flatNumber;
        public BlockOfFlats _blockOfFlats;

        public List<Flat> Flats { get; set; }

        public int EntranceNumber { get; }

        public Entrance(BlockOfFlats blockOfFlats) : this(0, 0, 0, blockOfFlats)
        {
            
        }
        public Entrance(int entranceNumber, int floors, int flatsPerFloor, BlockOfFlats blockOfFlats, bool auto = false)
        {
            Flats = new List<Flat>();
            EntranceNumber = entranceNumber;
            _floors = floors;
            _flatsPerFloor = flatsPerFloor;
            _flatNumber = 0;
            _blockOfFlats = blockOfFlats;

            if(!auto)
                CreateFlats(floors,floors);
        }

        public void ChangeFloors(int floors = 0)
        {
            CreateFlats(_floors, floors);
            _floors = floors;
        }

        public List<Resident> GetEntranceResidents()
        {
            var buffer = new List<Resident>();
            foreach (var flat in Flats)
            {
                buffer.AddRange(flat.Residents);
            }

            return buffer;
        }

        private void CreateFlats(int oldFloors, int newFloors)
        {
            var lel = this;
            if (Flats.Count <= 0)
            {
                for (var floor = 0; floor < _floors; floor++)
                {
                    for (var flatNumber = 0; flatNumber < _flatsPerFloor; flatNumber++)
                    {
                        Flats.Add(new Flat(_flatNumber, floor, lel));
                        _flatNumber++;
                    }
                }
            }
            
            else
            {
                _flatNumber = Flats[^1].FlatNumber;
                for (var floor = oldFloors; floor < newFloors; floor++)
                {
                    for (var flatNumber = 0; flatNumber < _flatsPerFloor; flatNumber++)
                    {
                        Flats.Add(new Flat(_flatNumber, floor, lel));
                        _flatNumber++;
                    }
                }
                
            }
        }
    }
}
