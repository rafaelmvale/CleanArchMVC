using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Application.Products.Queries;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public ProductService(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            //var productsEntity = await _productRepository.GetProductsAsync();
            //return _mapper.Map<IEnumerable<ProductDTO>>(productsEntity);

            var productsQuery = new GetProductsQuery();

            if (productsQuery == null)
                throw new Exception($"Entity could not be loaded");

            var result = await _mediator.Send(productsQuery);

            return _mapper.Map<IEnumerable<ProductDTO>>(result);
        }

        public async Task<ProductDTO> GetById(int? id)
        {
            var productByIdQuery = new GetProductsByIdQuery(id.Value);

            if (productByIdQuery == null)
                throw new Exception($"Entity could not be loaded");

            var result = await _mediator.Send(productByIdQuery);

            return _mapper.Map<ProductDTO>(result);
        }

        //public async Task<ProductDTO> GetPRoductCategory(int? id)
        //{
        //    var productByIdQuery = new GetProductsByIdQuery(id.Value);

        //    if (productByIdQuery == null)
        //        throw new Exception($"Entity could not be loaded");

        //    var result = await _mediator.Send(productByIdQuery);

        //    return _mapper.Map<ProductDTO>(result);
        //}

        public async Task Add(ProductDTO productDTO)
        {
            var productreateCommand  = _mapper.Map<ProductCreateCommand>(productDTO);
            await _mediator.Send(productreateCommand);
        }

        public async Task Update(ProductDTO productDTO)
        {
            var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDTO);
            await _mediator.Send(productUpdateCommand);
        }

        public async Task Remove(int? id)
        { 
            var  productRemoveCommand = new ProductRemoveCommand(id.Value);

            if (productRemoveCommand == null) throw new Exception($"Entity could not be loaded");

            await _mediator.Send(productRemoveCommand);
        }
    }
}
