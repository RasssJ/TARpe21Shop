using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TARpe21ShopVaitmaa.Core.Domain;
using TARpe21ShopVaitmaa.Core.Dto;

namespace TARpe21ShopVaitmaa.Core.ServiceInterface
{
    public interface ICarServices
    {
        Task<Car> Create(CarDto dto);
        //Task<Spaceship> GetUpdate(Guid id);         - not needed
        Task<Car> Update(CarDto dto);
        Task<Car> Delete(Guid Id);
        Task<Car> GetAsync(Guid Id);
    }
}
