using GarageVersion3.Core;
using GarageVersion3.Web.Validations;
using System.ComponentModel.DataAnnotations;

namespace GarageVersion3.Web.Models
{
#nullable disable
    public class MemberCreateViewModel
    {
        [Key]
        //[ToYoungToDrive (ErrorMessage = "You are to young to drive a car.")]
        public string PersNrId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //Navigation properties
        public ICollection<Vehicle> Vehicles { get; set; } /*= new List<Vehicle>();*/
    }
}
