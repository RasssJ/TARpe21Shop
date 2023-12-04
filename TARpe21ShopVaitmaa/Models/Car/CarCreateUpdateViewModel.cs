using Microsoft.AspNetCore.Mvc;
using TARpe21ShopVaitmaa.Core.Domain;
using TARpe21ShopVaitmaa.Core.Dto;
using TARpe21ShopVaitmaa.Models.Spaceship;

namespace TARpe21ShopVaitmaa.Models.Car
{
    public class CarCreateUpdateViewModel
    {


        public Guid? Id { get; set; } 
        public string Manufacturer { get; set; }
        public DateTime Year { get; set; } 
        public string Transmission { get; set; } 
        public int PriceOfVehicle { get; set; } 
        public int Weight { get; set; }
        public int MaxKMH { get; set; }
        public string BodyType { get; set; } 

        public List<FileToApiViewM> FileToApiViewModels { get; set; } = new List<FileToApiViewM>(); 

        public List<IFormFile> Files { get; set; }
        public List<ImageViewModel> Image { get; set; } = new List<ImageViewModel>(); 

        //database only properties
        public DateTime CreatedAt { get; set; } 
        public DateTime ModifiedAt { get; set; } 
    }
}

