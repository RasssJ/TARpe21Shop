using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TARpe21ShopVaitmaa.Core.Dto;

namespace TARpe21ShopVaitmaa.Core.Domain
{
    public class Car
    {

        public Guid Id { get; set; } 
        public string Manufacturer { get; set; }
        public DateTime Year { get; set; } 
        public string Transmission { get; set; } 
        public int PriceOfVehicle { get; set; } 
        public int Weight { get; set; }

        public int MaxKMH { get; set; }
        public int CarPassengerCount { get; set; } 
        public string BodyType { get; set; } 
        public IEnumerable<FileToApi> FilesToApi { get; set; } = new List<FileToApi>(); 



        public DateTime CreatedAt { get; set; } 
        public DateTime ModifiedAt { get; set; } 

    }
}

