using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using NewWebApp.Data;
using NewWebApp.Dto;
using NewWebApp.Models;

namespace NewWebApp.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public StorageRepository(IMapper mapper, IMemoryCache cache)
        {
            _mapper = mapper;
            _cache = cache;
        }

        public int AddStorage(StorageDto storage)
        {
            using (var context = new StorageContext())
            {
                if (context.Storages.Any(x => x.ProductId == storage.ProductId))
                    throw new Exception("Storage with this product is already exist!");

                var entity = _mapper.Map<Storage>(storage);

                context.Add(entity);
                context.SaveChanges();
                _cache.Remove("storages");

                return entity.Id;
            }
        }

        public StorageDto DeleteStorage(int productId)
        {
            using (var context = new StorageContext())
            {
                var storage = context.Storages.FirstOrDefault(x => x.ProductId == productId);

                if (storage == null)
                    throw new Exception("Storage not found!");

                var entity = _mapper.Map<StorageDto>(storage);

                context.Remove(entity);
                context.SaveChanges();
                _cache.Remove("storages");

                return entity;
            }
        }

        public IEnumerable<StorageDto> GetStorages()
        {
            using (var context = new StorageContext())
            {
                if (_cache.TryGetValue("storages", out List<StorageDto> list))
                    return list;

                list = context.Storages.Select(_mapper.Map<StorageDto>).ToList();

                _cache.Set("storages", list, TimeSpan.FromMinutes(30));

                return list;
            }
        }
    }
}
