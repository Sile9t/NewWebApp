using Microsoft.AspNetCore.Mvc;
using NewWebApp.Dto;
using NewWebApp.Repositories;

namespace NewWebApp.Controllers
{
    [ApiController]
    [Route("[controlle]")]
    public class StorageController : ControllerBase
    {
        private readonly IStorageRepository _repository;

        public StorageController(IStorageRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("add_product_to_storage")]
        public ActionResult<int> AddProductToStorage(StorageDto storage)
        {
            try
            {
                return Ok(_repository.AddStorage(storage));
            }
            catch (Exception ex) { return StatusCode(409, ex.Message); }
        }

        [HttpGet("get_storages")]
        public ActionResult GetStorages()
        {
            try
            {
                return Ok(_repository.GetStorages());
            }
            catch (Exception ex) { return StatusCode(409, ex.Message); }
        }

        [HttpDelete("delete_storage")]
        public ActionResult<StorageDto> DeleteStorage(int productId)
        {
            try
            {
                return Ok(_repository.DeleteStorage(productId));
            }
            catch (Exception ex) { return StatusCode(409, ex.Message); }
        }
    }
}
