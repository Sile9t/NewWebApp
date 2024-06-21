using NewWebApp.Abstractions;
using NewWebApp.Dto;
using NewWebApp.Models;

namespace NewWebApp.Graph.Query
{
    public class Query (IProductRepository productRepository)
    {
        public IEnumerable<ProductDto> GetProducts() => productRepository.GetAllProducts();
        public IEnumerable<ProductGroupDto> GetProductGroups([Service] ProductGroupRepository groupRepository) 
            => groupRepository.GetAllProductGroups();
    }
}
