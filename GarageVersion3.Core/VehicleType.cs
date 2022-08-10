namespace GarageVersion3.Core
{
    public class VehicleType
    {
#nullable disable

        private VehicleType()
        {
            KindOfVehicle = null!;
        }
        public VehicleType(string kindOfVehicle)
        {
            KindOfVehicle = kindOfVehicle; 
        }

        public int Id { get; set; }
        public string KindOfVehicle { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();   

    }
}