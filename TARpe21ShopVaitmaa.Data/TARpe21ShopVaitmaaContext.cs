using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TARpe21ShopVaitmaa.Core.Domain;
namespace TARpe21ShopVaitmaa.Data
{
    public class TARpe21ShopVaitmaaContext : DbContext
    {
        public TARpe21ShopVaitmaaContext(DbContextOptions<TARpe21ShopVaitmaaContext> options) : base(options) { }
        public DbSet<Spaceship> Spaceships { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<FileToDatabase> FilesToDatabase { get; set; }
        public DbSet<RealEstate> RealEstates { get; set; }
        public DbSet<FileToApi> FilesToApi { get; set; }
    }
}