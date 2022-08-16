using GarageVersion3.Core;
using System.ComponentModel.DataAnnotations;
namespace GarageVersion3.Web.Models
{
#nullable disable
    public class MemberEditViewModel
    {
        [Key]
        public string PersNrId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string FullName => $"{FirstName} {LastName}";

        //Navigation properties
        //public ICollection<Vehicle> Vehicles { get; set; }/* = new List<Vehicle>();*/
    }
}
