using NewWebApp.Dto;

namespace NewWebApp.Abstractions
{
    public interface IProductRepository
    {
        int AddProduct(ProductDto product);
        IEnumerable<ProductDto> GetAllProducts();
        ProductDto DeleteProduct(string name);
        string GetCsv();
    }
}
