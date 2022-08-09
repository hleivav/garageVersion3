namespace GarageVersion3.Core
{
    public class VehicleType
    {
#nullable disable
        public int Id { get; set; }
        public string KindOfVehicle { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}