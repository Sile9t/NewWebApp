using NewWebApp.Dto;

namespace NewWebApp.Repositories
{
    public interface IProductGroupRepository
    {
        int AddProductGroup(ProductGroupDto group);
        List<ProductGroupDto> GetAllProductGroups();
        ProductGroupDto DeleteProductGroup(string name);
    }
}
