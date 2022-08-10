using System.ComponentModel.DataAnnotations;

namespace GarageVersion3.Core
{
#nullable disable
    public class Vehicle
    {
        private Vehicle()
        {
            RegNrId = null!; 
        }

        public Vehicle(string regNrId, bool inGarage, DateTime startingAt)
        {
            RegNrId = regNrId;
            InGarage = inGarage;    
            StartingAt = startingAt;    
        }
        [Key]
        public string RegNrId  { get; set; }
        public bool InGarage { get; set; }
        public DateTime StartingAt { get; set; }

        //Foreign Key
        public string MemberId { get; set; }
        public int VehicleTypeId { get; set; }
        public int? ParkingSpotId { get; set; }

        //Navigation Property
        public Member Member { get; set; }
        public VehicleType VehicleType { get; set; }
        public ParkingSpot ParkingSpot { get; set; }
    }
}