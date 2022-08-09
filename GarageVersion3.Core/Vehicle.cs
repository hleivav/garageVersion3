using System.ComponentModel.DataAnnotations;

namespace GarageVersion3.Core
{
#nullable disable
    public class Vehicle
    {
        public Vehicle()
        {
            RegNrId = null; 
        }

        public Vehicle(string regNrId, int parkingNr, bool inGarage, DateTime startingAt)
        {
            RegNrId = regNrId;
            ParkingNr = parkingNr; 
            InGarage = inGarage;    
            StartingAt = startingAt;    
        }
        [Key]
        public string RegNrId  { get; set; }
        public int ParkingNr { get; set; }
        public bool InGarage { get; set; }
        public DateTime StartingAt { get; set; }

        //Foreign Key
        public string MemberId { get; set; }
        public int VehicleTypeId { get; set; }
        public int ParkingId { get; set; }

        //Navigation Property
        public Member Member { get; set; }
        public VehicleType VehicleType { get; set; }
        public ParkingSpot ParkingSpot { get; set; }
    }
}