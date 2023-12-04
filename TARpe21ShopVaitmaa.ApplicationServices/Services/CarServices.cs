using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using TARpe21ShopVaitmaa.Core.Domain;
using TARpe21ShopVaitmaa.Core.Dto;
using TARpe21ShopVaitmaa.Core.ServiceInterface;
using TARpe21ShopVaitmaa.Data;



namespace TARpe21ShopVaitmaa.ApplicationServices.Services
{
    public class CarServices : ICarServices
    {
        private readonly TARpe21ShopVaitmaaContext _context;
        private readonly IFilesServices _filesServices;
        public CarServices(TARpe21ShopVaitmaaContext context, IFilesServices filesServices)
        {
            _context = context;
            _filesServices = filesServices;
            
        }

        public async Task<Car> Create(CarDto dto)
        {
            Car car = new();

            car.Id = Guid.NewGuid();
            car.Manufacturer = dto.Manufacturer;
            car.Year = dto.Year;
            car.PriceOfVehicle = dto.PriceOfVehicle;
            car.Transmission = dto.Transmission;
            car.MaxKMH = dto.MaxKMH;
            car.Weight = dto.Weight;
            car.BodyType = dto.BodyType;
            car.CreatedAt = DateTime.Now;
            car.ModifiedAt = DateTime.Now;
            _filesServices.FilesToApi(dto, car);

            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
            return car;

        }
        public async Task<Car> Update(CarDto dto)
        {
            Car car = new();

            car.Id = dto.Id;
            car.Manufacturer = dto.Manufacturer;
            car.Year = dto.Year;
            car.PriceOfVehicle = dto.PriceOfVehicle;
            car.Transmission = dto.Transmission;
            car.MaxKMH = dto.MaxKMH;
            car.Weight = dto.Weight;
            car.BodyType = dto.BodyType;
            car.CreatedAt = DateTime.Now;
            car.ModifiedAt = DateTime.Now;
            _filesServices.FilesToApi(dto, car);
            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
            return car;
        }
        public async Task<Car> GetAsync(Guid id)
        {
            var result = await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
        public async Task<Car> Delete(Guid id)
        {
            var carId = await _context.Cars
                .Include(x => x.FilesToApi)
                .FirstOrDefaultAsync(x => x.Id == id);

            var images = await _context.FilesToApi
                .Where(x => x.CarId == id)
                .Select(y => new FileToApiDto
                {
                    Id = y.Id,
                    CarId = y.CarId,
                    ExistingFilePath = y.ExistingFilePath
                }).ToArrayAsync();
            await _filesServices.RemoveImagesFromApi(images);
            _context.Cars.Remove(carId);
            await _context.SaveChangesAsync();
            return carId;
        }
    }
}
