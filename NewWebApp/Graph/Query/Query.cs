using NewWebApp.Dto;
using NewWebApp.Models;
using NewWebApp.Repositories;

namespace NewWebApp.Graph.Query
{
    public class Query (IProductRepository productRepository)
    {
        public IEnumerable<ProductDto> GetProducts() => productRepository.GetAllProducts();
        public IEnumerable<ProductGroupDto> GetProductGroups([Service] IProductGroupRepository groupRepository) 
            => groupRepository.GetAllProductGroups();
    }
}
