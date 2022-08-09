using System.ComponentModel.DataAnnotations;

namespace GarageVersion3.Core
{
    public class Member
    {
        public Member()
        {
            PersNrId = null!;
            FirstName = null!;
            LastName = null!;
        }
        public Member(string persNrId, string firstName, string lastName)
        {
            PersNrId = persNrId;
            FirstName = firstName;
            LastName = lastName;
        }
        [Key]
        public string PersNrId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        //Navigation properties
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}