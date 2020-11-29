using System.Collections.Generic;
using System.Linq;

namespace HousingEstateConsole
{
    public class Entrance : IShowable
    {
        public string ParentId { get; set; }
        public int _floors;
        public int _flatsPerFloor;
        public int _flatNumber;
        public int area;
        public int rooms;

        
        public BlockOfFlats _blockOfFlats;

        public List<Flat> Flats { get; set; }

        public int EntranceNumber { get; }
        
        public Entrance(int entranceNumber, int floors, int flatsPerFloor, int area, int rooms, BlockOfFlats blockOfFlats, bool auto = false)
        {
            Flats = new List<Flat>();
            EntranceNumber = entranceNumber;
            _floors = floors;
            _flatsPerFloor = flatsPerFloor;
            _flatNumber = 0;
            _blockOfFlats = blockOfFlats;

            this.rooms = rooms;
            this.area = area;

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

        public void CreateFlats(int oldFloors, int newFloors)
        {
            var lel = this;
            if (Flats.Count <= 0)
            {
                for (var floor = 0; floor < _floors; floor++)
                {
                    for (var flatNumber = 0; flatNumber < _flatsPerFloor; flatNumber++)
                    {
                        Flats.Add(new Flat(_flatNumber, floor, area, rooms, lel));
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
                        Flats.Add(new Flat(_flatNumber, floor, area, rooms, lel));
                        _flatNumber++;
                    }
                }
                
            }
        }

        public void Add(List<object> variables)
        {
            
        }

        public string Show()
        {
            return $"This is entrance with number {EntranceNumber}. There is {GetEntranceResidents().Count} residents in {Flats.Count} flats.";
        }

        public void Change(string what, string to)
        {
            
        }

        public IShowable GetParent()
        {
            return _blockOfFlats;
        }

        public string GetWriteName()
        {
            return EntranceNumber.ToString();
        }

        public string GetStructure()
        {
            return Flats.Aggregate("", (current, flat) => current + (flat.FlatNumber + "\n"));
        }

        public Entrance()
        {

        }
    }
}
