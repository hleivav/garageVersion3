namespace GarageVersion3.Core
{
    public class ParkingSpot
    {
        public ParkingSpot(int spotNr)
        {
            SpotNr = spotNr;
        }
        public int Id { get; set; }
        public int SpotNr { get; set; }        
    }
}