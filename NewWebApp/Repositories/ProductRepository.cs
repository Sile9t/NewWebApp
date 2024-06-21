using AutoMapper;
using NewWebApp.Data;
using NewWebApp.Dto;
using NewWebApp.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace NewWebApp.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public ProductRepository(IMapper mapper, IMemoryCache cache)
        {
            _mapper = mapper;
            _cache = cache;
        }

        public int AddProduct(ProductDto product)
        {
            using (var context = new StorageContext())
            {
                if (context.Products.Any(x => x.Name == product.Name))
                    throw new Exception("Product is already exist!");

                var entity = _mapper.Map<Product>(product);

                context.Add(entity);
                context.SaveChanges();
                _cache.Remove("products");

                return entity.Id;
            }
        }

        public ProductDto DeleteProduct(string name)
        {
            using (var context = new StorageContext())
            {
                var product = context.Products.FirstOrDefault(x => x.Name == name);
                if (product == null)
                    throw new Exception("Product not found!");

                var entity = _mapper.Map<ProductDto>(product);

                context.Products.Remove(product);
                context.SaveChanges();
                _cache.Remove("products");

                return entity;
            }
        }

        public IEnumerable<ProductDto> GetAllProducts()
        {
            using (var context = new StorageContext())
            {
                if (_cache.TryGetValue("products", out List<ProductDto> listDto))
                    return listDto;

                listDto = context.Products.Select(_mapper.Map<ProductDto>).ToList();

                _cache.Set("products", listDto, TimeSpan.FromMinutes(30));

                return listDto;
            }
        }

        public string GetCsv()
        {
            var products = GetAllProducts();

            StringBuilder sb = new StringBuilder();

            foreach (var p in products)
            {
                sb.Append("Name: " + p.Name);
                sb.Append(", Price: " + p.Price);
                sb.Append(", Descroption: " + p.Description + "\n");
            }

            return sb.ToString();
        }
    }
}
