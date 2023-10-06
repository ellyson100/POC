using Microsoft.AspNetCore.Mvc;
using POC.API.Interfaces.Services;
using POC.API.Model;
using POC.API.ViewModels;

namespace POC.API.Controllers
{
    [Route("api/[controller]s/")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public ActionResult<Product> Create(ProductCreatedViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                var entity = _productService.Create(viewModel.ToEntity());

                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            try
            {
                var entity = _productService.GetById(id);

                if (entity == null) return BadRequest();

                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<Product> GetAll()
        {
            try
            {
                var entity = _productService.GetAll();

                if (entity == null) return BadRequest();

                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Product> Update(ProductViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                var entity = _productService.Update(viewModel.ToEntity());

                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Product> Delete(int id)
        {
            try
            {
                _productService.Delete(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
