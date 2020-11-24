using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HousingEstateConsole
{
    public class Flat
    {
        public readonly Entrance Entrance;

        public List<Resident> Residents { get; private set; }

        public int FlatNumber { get; }

        public int FlatFloor { get; }

        public Flat(int flatNumber, int flatFloor, Entrance entrance)
        {
            FlatNumber = flatNumber;
            FlatFloor = flatFloor;
            Residents = new List<Resident>();
            Entrance = entrance;
        }

        public void AddResident(Resident resident)
        {
            Residents.Add(resident);
        }
        
        public void RemoveResident(string name)
        {
            var buffer = Residents.Where(resident => resident.GetFullName() != name).ToList();
            Residents.Clear();
            Residents = buffer;
        }

        public string GetData()
        {
            return $"flatNumber: {FlatNumber}\nflatFloor: {FlatFloor}";
        }
    }
    
    //TODO: rozloha bytu a izby bytu
}