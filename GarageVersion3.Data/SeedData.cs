using Bogus;
using GarageVersion3.Core;
using GarageVersion3.Web.Data;
using Microsoft.EntityFrameworkCore;

public class SeedData
{
    private static Faker faker = null; 
    public static async Task InitAsync(GarageVersion3Context db)
    {
       // Console.WriteLine(GeneratePersonNr()); ;

        if (await db.Member.AnyAsync()) return;
        faker = new Faker("sv");

        var parkingSpots = GenerateParkingSpots(100);
        await db.AddRangeAsync(parkingSpots); 

        var vehicleTypes = GenerateVehicleTypes();
        await db.AddRangeAsync(vehicleTypes);

        var members = GenerateMembers(10, vehicleTypes);
        await db.AddRangeAsync(members);


        await db.SaveChangesAsync();    
    }

    private static IEnumerable<ParkingSpot> GenerateParkingSpots(int NoOfParkingSpots)
    {
        var parkingSpots = new List<ParkingSpot>();
        for (int i = 1; i <= NoOfParkingSpots; i++)
        {
            var parkingSpot = new ParkingSpot(i);
            parkingSpots.Add(parkingSpot);  
        }
        return parkingSpots;
    }

    private static IEnumerable<Member> GenerateMembers(int nrOfMembers, List<VehicleType> vehicleTypes)
    {
        var members = new List<Member>();   
        for (int i = 0; i < nrOfMembers; i++)
        {
            var persNr = GeneratePersonNr();
            var fName = faker.Name.FirstName();
            var lName = faker.Name.LastName();
            var member = new Member(persNr, fName, lName);

            member.Vehicles = GenerateVehicles(1, vehicleTypes);

            //varje medlem behöver registreras med ett fordon
            //fordonet behöver vara komplett inom den kopplas till en medlem, vilket innebär: 
            //ett fejkat registreringsnr /fixat
            //en fejkad status om bilen är parkerad eller ej
            //ett fejkad datum för när bilen har parkerats om den fejkade statuset säger att bilen är parkerad--   
            //
            members.Add(member);
        }
        return members;
    }

    private static List<Vehicle> GenerateVehicles(int nrOfVehicles, List<VehicleType> vehicleTypes)
    {
        var vehicles = new List<Vehicle>();
        Random rnd = new Random();

        var regNr = GenerateRegNr();
        var isParked = false; //Bilarna som inte är parkerade bör inte ha en parkeringstid.
        var startingTime = DateTime.Now; //Innebär att den här variabeln borde kunna vara null.//FIXA HÄRRRRRRRRRRRRRRRRRRRRRRRRRRR/////
        
        var vehicle = new Vehicle(regNr, isParked, startingTime);  
        vehicle.VehicleType = vehicleTypes[rnd.Next(vehicleTypes.Count)];
        vehicles.Add(vehicle);
        return vehicles;
    }

    private static List<VehicleType> GenerateVehicleTypes()
    {
        var vehicleTypes = new List<VehicleType>();
        vehicleTypes.Add(new VehicleType("Car"));
        vehicleTypes.Add(new VehicleType("Truck"));
        vehicleTypes.Add(new VehicleType("Bus"));
        vehicleTypes.Add(new VehicleType("Motorbike"));
        vehicleTypes.Add(new VehicleType("Tractor"));
        return vehicleTypes;
    }

    //PersNrGenerator
    static string GeneratePersonNr()
    {
        string personnr = "";
        DateTime start = new DateTime(1930, 1, 1);
        DateTime end = new DateTime(2004, 08, 10);
        int range = (end - start).Days;


        Random randomDay = new Random();
        DateTime theRandomday = start.AddDays(randomDay.Next(range));
        personnr = theRandomday.ToString().Substring(0, 10);
        personnr = personnr.Replace("-", string.Empty);
        personnr = personnr + "-";
        for (int i = 0; i <= 3; i++)
        {
            Random rnd = new Random();
            int rndNr = rnd.Next(0,10);
            personnr = personnr + rndNr.ToString();
        }

        return personnr;
    }
    //Regnr generator
    private static string GenerateRegNr()
    {
        string regNr = "";
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        Random randomLetter = new Random();
        Random randomNumber = new Random();
        for (int i = 0; i < 3; i++)
        {
            regNr = regNr + alphabet[randomLetter.Next(alphabet.Length)];
        }
        for (int i = 0; i < 3; i++)
        {
            regNr = regNr + randomNumber.Next(9).ToString();
        }
        return regNr;
    }
}