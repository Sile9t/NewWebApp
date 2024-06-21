using AutoMapper;
using NewWebApp.Data;
using NewWebApp.Dto;
using NewWebApp.Models;
using Microsoft.Extensions.Caching.Memory;

namespace NewWebApp.Abstractions
{
    public class ProductGroupRepository : IProductGroupRepository
    {
        private readonly StorageContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public ProductGroupRepository(StorageContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        public int AddProductGroup(ProductGroupDto group)
        {
            if (_context.ProductGroups.Any(x => x.Name == group.Name))
                throw new Exception("Group is already exist!");

            var entity = _mapper.Map<ProductGroup>(group);

            _context.Add(entity);
            _context.SaveChanges();
            _cache.Remove("groups");

            return entity.Id;
        }

        public ProductGroupDto DeleteProductGroup(string name)
        {
            var group = _context.ProductGroups.FirstOrDefault(x => x.Name == name);

            if (group == null)
                throw new Exception("No group like this!");

            var entity = _mapper.Map<ProductGroupDto>(group);

            _context.Remove(group);
            _context.SaveChanges();
            _cache.Remove("groups");

            return entity;
        }

        public List<ProductGroupDto> GetAllProductGroups()
        {
            if (_cache.TryGetValue("groups", out List<ProductGroupDto> list))
                return list;

            list = _context.ProductGroups.Select(_mapper.Map<ProductGroupDto>).ToList();

            _cache.Set("groups", list, TimeSpan.FromMinutes(30));

            return list;
        }
    }
}
