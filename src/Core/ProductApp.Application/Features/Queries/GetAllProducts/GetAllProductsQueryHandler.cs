using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ProductApp.Application.Dto;
using ProductApp.Application.Features.Queries.GetProductById;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Wrappers;

namespace ProductApp.Application.Features.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ServiceResponse<List<GetProductByIdViewModel>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetProductByIdViewModel>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();

            var productsByIdVM= _mapper.Map<List<GetProductByIdViewModel>>(products);

            return new ServiceResponse<List<GetProductByIdViewModel>>(productsByIdVM);

        }
    }
}
