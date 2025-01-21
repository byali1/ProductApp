using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductApp.Application.Features.Commands.UpdateProduct;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Interfaces.Repository
{
    public interface IProductRepository : IGenericRepositoryAsync<Product>
    {
        //Ek operasyon imzaları
        Task<UpdateProductQuantityViewModel> UpdateProductQuantityAsync(UpdateProductQuantityViewModel updateProductQuantityVM);
    }
}
