using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GarageVersion3.Core;

namespace GarageVersion3.Web.Data
{
    public class GarageVersion3Context : DbContext
    {
        public GarageVersion3Context (DbContextOptions<GarageVersion3Context> options)
            : base(options)
        {
        }

        public DbSet<GarageVersion3.Core.Member> Member { get; set; } = default!;

        public DbSet<GarageVersion3.Core.Vehicle>? Vehicle { get; set; }

        public DbSet<GarageVersion3.Core.VehicleType>? VehicleType { get; set; }
    }
}
