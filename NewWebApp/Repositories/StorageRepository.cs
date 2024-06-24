using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using NewWebApp.Dto;

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
            throw new NotImplementedException();
        }

        public StorageDto DeleteStorage(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StorageDto> GetStorages()
        {
            throw new NotImplementedException();
        }
    }
}
