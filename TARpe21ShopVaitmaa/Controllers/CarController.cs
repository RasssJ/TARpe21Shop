using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TARpe21ShopVaitmaa.ApplicationServices.Services;
using TARpe21ShopVaitmaa.Core.Domain;
using TARpe21ShopVaitmaa.Core.Dto;
using TARpe21ShopVaitmaa.Core.ServiceInterface;
using TARpe21ShopVaitmaa.Data;
using TARpe21ShopVaitmaa.Models.Car;
using TARpe21ShopVaitmaa.Models.Spaceship;

namespace TARpe21ShopVaitmaa.Controllers
{
    public class CarController : Controller
    {
        private readonly TARpe21ShopVaitmaaContext _context;
        private readonly ICarServices _carService;
        private readonly IFilesServices _filesServices;
        public CarController(ICarServices carService, TARpe21ShopVaitmaaContext context, IFilesServices filesServices)
        {
            _carService = carService;
            _context = context;
            _filesServices = filesServices;
        }
        public async Task<IActionResult> Index()
        {
            var result = _context.Cars
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new CarIndexViewModel
                {

                    Id = x.Id,
                    Manufacturer = x.Manufacturer,   
                    Year = x.Year,
                    PriceOfVehicle = x.PriceOfVehicle,
                    Transmission = x.Transmission,
                    MaxKMH = x.MaxKMH,
                    Weight = x.Weight,
                    BodyType = x.BodyType,
                });
                    
            return View(result);

        }
        [HttpGet]
        public IActionResult Create()
        {
            CarCreateUpdateViewModel vm = new ();
             return View("CreateUpdate", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CarCreateUpdateViewModel vm)
        {
            var dto = new CarDto()
            {
                Id = Guid.NewGuid(),
                Manufacturer = vm.Manufacturer,
                Year = vm.Year,
                PriceOfVehicle = vm.PriceOfVehicle,
                Transmission = vm.Transmission,
                MaxKMH = vm.MaxKMH,
                Weight = vm.Weight,
                BodyType = vm.BodyType,
                Files = vm.Files,
                FileToApiDtos = vm.FileToApiViewModels
                .Select(x => new FileToApiDto
                {
                    Id = x.ImageId,
                    ExistingFilePath = x.FilePath,
                    CarId = x.CarId,
                }).ToArray()
            };
            var result = await _carService.Create(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), vm);

        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var car = await _carService.GetAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            var images = await _context.FilesToApi
                .Where(x => x.CarId == id)
                .Select(y => new FileToApiViewM
                {
                    FilePath = y.ExistingFilePath,
                    ImageId = y.Id
                }).ToArrayAsync();
            var vm = new CarCreateUpdateViewModel();
            
            vm.Id = car.Id;
            vm.Manufacturer = car.Manufacturer;
            vm.Year = car.Year;
            vm.PriceOfVehicle = car.PriceOfVehicle;
            vm.Transmission = car.Transmission;
            vm.MaxKMH = car.MaxKMH;
            vm.Weight = car.Weight;
            vm.BodyType = car.BodyType;
            vm.FileToApiViewModels.AddRange(images);


            return View("CreateUpdate", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CarCreateUpdateViewModel vm)
        {
            var dto = new CarDto()
            {
                Id = (Guid)vm.Id,
                Manufacturer = vm.Manufacturer,
                Year = vm.Year,
                PriceOfVehicle = vm.PriceOfVehicle,
                Transmission = vm.Transmission,
                MaxKMH = vm.MaxKMH,
                Weight = vm.Weight,
                BodyType = vm.BodyType,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = DateTime.Now,
                Files = vm.Files,
                FileToApiDtos = vm.FileToApiViewModels
                .Select(x => new FileToApiDto
                {
                    Id = x.ImageId,
                    ExistingFilePath = x.FilePath,
                    CarId = x.CarId,
                }).ToArray()
            };
            var result = await _carService.Update(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), vm);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var car = await _carService.GetAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            var images = await _context.FilesToApi
                .Where(x => x.CarId == id)
                .Select(y => new FileToApiViewM
                {
                    FilePath = y.ExistingFilePath,
                    ImageId = y.Id
                }).ToArrayAsync();

            var vm = new CarDetailsViewModel();

            vm.Id = car.Id;
            vm.Manufacturer = car.Manufacturer;
            vm.Year = car.Year;
            vm.PriceOfVehicle = car.PriceOfVehicle;
            vm.Transmission = car.Transmission;
            vm.MaxKMH = car.MaxKMH;
            vm.Weight = car.Weight;
            vm.BodyType = car.BodyType;
            vm.CreatedAt = car.CreatedAt;
            vm.ModifiedAt = car.ModifiedAt;
            vm.FileToApiViewModels.AddRange(images);

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var car = await _carService.GetAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            var images = await _context.FilesToApi
                .Where(x => x.Id == id)
                .Select(y => new FileToApiViewM
                {
                    FilePath = y.ExistingFilePath,
                    ImageId = y.Id,

                }).ToArrayAsync();

                

            var vm = new CarDeleteViewModel();

            vm.Id = car.Id;
            vm.Manufacturer = car.Manufacturer;
            vm.Year = car.Year;
            vm.PriceOfVehicle = car.PriceOfVehicle;
            vm.Transmission = car.Transmission;
            vm.MaxKMH = car.MaxKMH;
            vm.Weight = car.Weight;
            vm.BodyType = car.BodyType;
            vm.FileToApiViewModels.AddRange(images);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var car = await _carService.Delete(id);
            if (car == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveImage(FileToApiViewM vm)
        {
            var dto = new FileToApiDto()
            {
                Id = vm.ImageId
            };
            var image = await _filesServices.RemoveImageFromApi(dto);
            if (image == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }


    }
}
