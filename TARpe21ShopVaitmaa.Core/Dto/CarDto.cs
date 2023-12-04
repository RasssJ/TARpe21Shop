using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TARpe21ShopVaitmaa.Core.Dto
{
    public class CarDto
    {
        public Guid Id { get; set; } 
        public string Manufacturer { get; set; }
        public DateTime Year { get; set; } 
        public string Transmission { get; set; } 
        public int PriceOfVehicle { get; set; } 
        public int Weight { get; set; }

        public int MaxKMH { get; set; }
        public string BodyType { get; set; } 
        public List<IFormFile> Files { get; set; }  
        public IEnumerable<FileToApiDto> FileToApiDtos { get; set; } = new List<FileToApiDto>(); 

        public IEnumerable<FileToDatabaseDto> Image { get; set; } = new List<FileToDatabaseDto>(); 


        //database only properties
        public DateTime CreatedAt { get; set; } 
        public DateTime ModifiedAt { get; set; } 
    }
}
