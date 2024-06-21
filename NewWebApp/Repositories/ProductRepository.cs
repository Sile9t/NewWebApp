using AutoMapper;
using NewWebApp.Abstractions;
using NewWebApp.Data;
using NewWebApp.Dto;
using NewWebApp.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace NewWebApp.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StorageContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public ProductRepository(StorageContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        public int AddProduct(ProductDto product)
        {
            if (_context.Products.Any(x => x.Name == product.Name))
                throw new Exception("Product is already exist!");

            var entity = _mapper.Map<Product>(product);

            _context.Add(entity);
            _context.SaveChanges();
            _cache.Remove("products");

            return entity.Id;
        }

        public ProductDto DeleteProduct(string name)
        {
            var product = _context.Products.FirstOrDefault(x => x.Name == name);
            if (product == null)
                throw new Exception("Product not found!");

            var entity = _mapper.Map<ProductDto>(product);

            _context.Products.Remove(product);
            _context.SaveChanges();
            _cache.Remove("products");

            return entity;
        }

        public IEnumerable<ProductDto> GetAllProducts()
        {
            if (_cache.TryGetValue("products", out List<ProductDto> listDto))
                return listDto;

            listDto = _context.Products.Select(_mapper.Map<ProductDto>).ToList();

            _cache.Set("products", listDto, TimeSpan.FromMinutes(30));

            return listDto;
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
