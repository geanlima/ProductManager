using Microsoft.AspNetCore.Mvc;
using ProductManager.Application.DTOs;
using ProductManager.Application.Interfaces;
using ProductManager.Application.QueryParameters;

namespace ProductManager.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var product = await _productService.GetByIdServiceAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await _productService.GetAllServiceAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDTO product)
        {
            try
            {
                await _productService.CreateServiceAsync(product);
                return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] ProductDTO product)
        {
            try
            {
                await _productService.UpdateServiceAsync(product);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("DeleteLogicProduct/{id}")]
        public async Task<ActionResult> DeactivateProduct(int id)
        {
            try
            {
                await _productService.DeleteLogicServiceAsync(id);
                return Ok("Produto deletado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao deletar o produto : {ex.Message}");
            }
        }


        [HttpGet("GetProducts")]
        public async Task<ActionResult<List<ProductDTO>>> GetProducts([FromQuery] ProductFilter filter, [FromQuery] PaginationParameters pagination)
        {
            var products = await _productService.GetProductsServiceAsync(filter, pagination);
            return Ok(products);
        }
        
    }
}
