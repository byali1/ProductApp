using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductApp.Application.Features.Commands.UpdateProduct;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Domain.Entities;
using ProductApp.Persistence.Context;

namespace ProductApp.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        //Özel operasyon
        public async Task<UpdateProductQuantityViewModel> UpdateProductQuantityAsync(UpdateProductQuantityViewModel updateProductQuantityVM)
        {
            var product = await _dbContext.Set<Product>().FindAsync(updateProductQuantityVM.Id);

            if (product == null)
            {
                throw new Exception($"Product with ID {updateProductQuantityVM.Id} not found");
            }

            int check = product.Quantity + updateProductQuantityVM.Quantity;

            if (check == 0 || check < 0)
                product.Quantity = 0;
            else
                product.Quantity += updateProductQuantityVM.Quantity;


            product.UpdatedDate = DateTimeOffset.UtcNow;
            _dbContext.Entry(product).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            updateProductQuantityVM.Quantity = product.Quantity;

            return updateProductQuantityVM;
        }
    }
}

