using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HousingEstateConsole
{
    public class Flat : IShowable
    {
        public readonly Entrance Entrance;

        public List<Resident> Residents { get; }

        public int FlatNumber { get; }

        public int FlatFloor { get; }
        
        private int FlatArea { get; set; }
        
        private int FlatRooms { get; set; }

        public Flat(int flatNumber, int flatFloor, int flatArea, int flatRooms,Entrance entrance)
        {
            FlatNumber = flatNumber;
            FlatFloor = flatFloor;
            Residents = new List<Resident>();
            Entrance = entrance;
            FlatArea = flatArea;
            FlatRooms = flatRooms;
        }
        
        public void Add(List<object> variables)
        {
            var resident = new Resident(variables[0] as string, variables[1] as string, int.Parse(variables[2] as string ?? throw new ArgumentException()), this);
            Residents.Add(resident);
        }

        public void Show()
        {
            
        }

        public void Change(string what, string to)
        {
            switch (what)
            {
                case "area":
                    FlatArea = int.Parse(to);

                    break;
                
                case "rooms":
                    FlatRooms = int.Parse(to);

                    break;
            }
        }

        public IShowable GetParent()
        {
            return Entrance;
        }

        public string GetWriteName()
        {
            return FlatNumber.ToString();
        }

        public string GetStructure()
        {
            var buffer = "";

            foreach (var resident in Residents)
            {
                buffer += resident.GetWriteName() + "\n";
            }

            return buffer;
        }
    }
    
    //TODO: rozloha bytu a izby bytu
}