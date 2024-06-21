using NewWebApp.Dto;

namespace NewWebApp.Repositories
{
    public interface IProductRepository
    {
        int AddProduct(ProductDto product);
        IEnumerable<ProductDto> GetAllProducts();
        ProductDto DeleteProduct(string name);
        string GetCsv();
    }
}
