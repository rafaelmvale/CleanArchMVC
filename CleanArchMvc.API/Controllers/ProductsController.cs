using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var products = await _productService.GetProducts();
            if (products == null)
            {
                return NotFound("Products Not Found");
            }

            return Ok(products);
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO product)
        {
            if (product == null)
            {
                return BadRequest("Data Invalid");
            }

            await _productService.Add(product);

            return new CreatedAtRouteResult("GetProduct",
                new { id = product.Id }, product);
        }
        [HttpPut("{id}")]

        public async Task<ActionResult> Put(int id, [FromBody] ProductDTO productDto)
        {
            if (id != productDto.Id)
            {
                return BadRequest("Data Invalid");
            }
            if (productDto == null) return BadRequest("Data Invalid");

            await _productService.Update(productDto);

            return Ok(productDto);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var productDto = await _productService.GetById(id);
            if (productDto == null)
            {
                return NotFound("Product not found");
            }

            await _productService.Remove(id);

            return Ok(productDto);
        }
    }
}
