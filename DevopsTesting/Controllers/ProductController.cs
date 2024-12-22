using DevopsTesting.Services;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace DevopsTesting.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet] 
        public ActionResult<IEnumerable<Product>>  GetAll(int top=0)
        {
          var item= _productService.GetProducts(top);
            return Ok(item);
        }
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var item=_productService.GetProductById(id);
            return Ok(item);
        }
        [HttpPost]
        public ActionResult<Product> Put (int id,[FromBody] Product product) {
            var item = _productService.Update(product);
           if (item==null) return NotFound();
            return Ok(item);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var item=_productService.Delete(id);
            if(!item) return BadRequest();
            return Ok(item);
        }
    }
}
