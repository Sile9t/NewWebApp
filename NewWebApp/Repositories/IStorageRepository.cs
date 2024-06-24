using NewWebApp.Dto;

namespace NewWebApp.Repositories
{
    public interface IStorageRepository
    {
        int AddStorage(StorageDto storage);
        IEnumerable<StorageDto> GetStorages();
        StorageDto DeleteStorage(int productId);
    }
}
