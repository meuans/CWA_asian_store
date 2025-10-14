using CWA_asian_store.Entity.Model;
using CWA_asian_store.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace CWA_asian_store.Controllers
{
   
        [ApiController]
        [Route("/")]



    public class ProductsController : ControllerBase
        {

      

        private readonly IProductService _productService;

            public ProductsController(IProductService productService)
            {
                _productService = productService;
            }

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var products = await _productService.GetAllAsync();
                return Ok(products);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
            {
                var product = await _productService.GetByIdAsync(id);
                if (product == null) return NotFound();
                return Ok(product);
            }

            [HttpPost]
            public async Task<IActionResult> Create(Product product)
            {
                await _productService.AddAsync(product);
                return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
            }
        }
    
}
