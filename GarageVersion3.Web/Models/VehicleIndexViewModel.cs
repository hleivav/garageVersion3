using System.ComponentModel.DataAnnotations;

namespace GarageVersion3.Web.Models
{
#nullable disable
    public class VehicleIndexViewModel
    {
        [Key]
        public string RegNrId { get; set; }
        public bool InGarage { get; set; }
        public DateTime StartingAt { get; set; }

        //Foreign Key
        public string MemberId { get; set; }
        public string MemberName { get; set; }  
        public string VehicleTypeName { get; set; }
        public int? ParkingSpotId { get; set; }
    }
}
