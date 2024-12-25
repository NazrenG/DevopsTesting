using DevopsTesting.Dtos;
using DevopsTesting.Services;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace DevopsTesting.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("AllProduct")] 
        public ActionResult<IEnumerable<Product>>  GetAll(int top=0)
        {
          var item= _productService.GetProducts(top);
            return Ok(item);
        }
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null) return NotFound();
             
            var result = new AddProduct
            {
                Name = product.Name,
                Price = product.Price
            };
            return Ok(result); 
        }
        [HttpPost("AddProduct")]
        public ActionResult<Product> Add( [FromBody] AddProduct product)
        {
            
            if (product == null) return NotFound();
            _productService.Add(new Product
            {
                Name = product.Name,
                Price = product.Price,
            });

            return Ok(product);
        }

        [HttpPut("UpdatedProduct/{id}")]
        public ActionResult<Product> Put (int id,[FromBody] AddProduct product) {
            var item=_productService.GetProductById(id);
            if (item == null) return NotFound();
            item.Price = product.Price;
            item.Name = product.Name;
            var products = new Product
            {
                Name = product.Name,
                Price = product.Price,
            };  
            return Ok(products);
        }

        [HttpDelete("DeleteProduct/{id}")]
        public ActionResult Delete(int id)
        {
            var item=_productService.Delete(id);
            if(!item) return BadRequest();
            return Ok(item);
        }
    }
}
