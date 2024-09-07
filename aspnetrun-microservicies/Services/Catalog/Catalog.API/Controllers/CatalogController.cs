using Catalog.API.Entities;
using Catalog.API.Repositoies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _repositroy;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository repositroy, ILogger<CatalogController> logger)
        {
            _repositroy = repositroy;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _repositroy.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)(int)HttpStatusCode.NotFound)]

        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await _repositroy.GetProduct(id);
            if (product == null)
            {
                _logger.LogError($"Product with id :{id} , not found");
                return NotFound();
            }

            return Ok(product);
        }
        [Route("[action]/category", Name = "GetProductByCategory")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductByCategory(string Category)
        {
            var products = await _repositroy.GetProductByCategory(Category);


            return Ok(products);
        }
        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _repositroy.CreateProduct(product);

            return CreatedAtRoute("GetProduct", new { id = product }, product);
        }
        [HttpPut]
        [ProducesResponseType(typeof(Product),
            (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> UpdateProduct([FromBody] Product product)
        {
            return Ok(await _repositroy.UpdateProduct(product));
        }
        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType((int)(int)HttpStatusCode.NotFound)]

        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> DeleteProduct(string id)
        {
            var product = await _repositroy.DeleteProduct(id);

            return Ok(product);
        }
    }
}
