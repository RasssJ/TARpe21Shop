using Microsoft.AspNetCore.Mvc;

namespace TARpe21ShopVaitmaa.Models.Car
{
    public class FileToApiViewM
    {
        public Guid ImageId { get; set; }
        public string FilePath { get; set; }
        public Guid RealEstateId { get; set; }
        public Guid? CarId { get; set; }
    }
}
