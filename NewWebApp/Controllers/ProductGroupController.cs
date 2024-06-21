using NewWebApp.Abstractions;
using NewWebApp.Dto;
using Microsoft.AspNetCore.Mvc;

namespace NewWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductGroupController : ControllerBase
    {
        private readonly IProductGroupRepository _repository;

        public ProductGroupController(IProductGroupRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("add_product_group")]
        public ActionResult<int> AddProductGroup(ProductGroupDto group)
        {
            try
            {
                return Ok(_repository.AddProductGroup(group));
            }
            catch (Exception ex) { return StatusCode(409, ex.Message); }
        }

        [HttpGet("get_all_product_groups")]
        public ActionResult<List<ProductGroupDto>> GetAllProductGroups()
        {
            try
            {
                return Ok(_repository.GetAllProductGroups());
            }
            catch (Exception ex) { return StatusCode(409, ex.Message); }
        }

        [HttpDelete("delete_product_group")]
        public ActionResult<ProductGroupDto> DeleteProductGroup(string name)
        {
            try
            {
                return Ok(_repository.DeleteProductGroup(name));
            }
            catch (Exception ex) { return StatusCode(409, ex.Message); }
        }
    }
}
