using AutoMapper;
using NewWebApp.Data;
using NewWebApp.Dto;
using NewWebApp.Models;
using Microsoft.Extensions.Caching.Memory;

namespace NewWebApp.Repositories
{
    public class ProductGroupRepository : IProductGroupRepository
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public ProductGroupRepository(IMapper mapper, IMemoryCache cache)
        {
            _mapper = mapper;
            _cache = cache;
        }

        public int AddProductGroup(ProductGroupDto group)
        {
            using (var context = new StorageContext())
            {
                if (context.ProductGroups.Any(x => x.Name == group.Name))
                    throw new Exception("Group is already exist!");

                var entity = _mapper.Map<ProductGroup>(group);

                context.Add(entity);
                context.SaveChanges();
                _cache.Remove("groups");

                return entity.Id;
            }
        }

        public ProductGroupDto DeleteProductGroup(string name)
        {
            using (var context = new StorageContext())
            {
                var group = context.ProductGroups.FirstOrDefault(x => x.Name == name);

                if (group == null)
                    throw new Exception("No group like this!");

                var entity = _mapper.Map<ProductGroupDto>(group);

                context.Remove(group);
                context.SaveChanges();
                _cache.Remove("groups");

                return entity;
            }
        }

        public List<ProductGroupDto> GetAllProductGroups()
        {
            using (var context = new StorageContext())
            {
                if (_cache.TryGetValue("groups", out List<ProductGroupDto> list))
                    return list;

                list = context.ProductGroups.Select(_mapper.Map<ProductGroupDto>).ToList();

                _cache.Set("groups", list, TimeSpan.FromMinutes(30));

                return list;
            }
        }
    }
}
