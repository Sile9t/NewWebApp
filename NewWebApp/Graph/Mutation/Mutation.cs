using NewWebApp.Dto;
using NewWebApp.Repositories;

namespace NewWebApp.Graph.Mutation
{
    public class Mutation (IProductRepository productRepository)
    {
        public int AddProduct(ProductDto product) 
            => productRepository.AddProduct(product);

        public ProductDto DeleteProduct(string name)
            => productRepository.DeleteProduct(name);

        public int AddProductGroup([Service] IProductGroupRepository groupRepository, ProductGroupDto group)
            => groupRepository.AddProductGroup(group);

        public ProductGroupDto DeleteProductGroup([Service] IProductGroupRepository groupRepository, string name)
            => groupRepository.DeleteProductGroup(name);

        public int AddStorage([Service] IStorageRepository storageRepository, StorageDto storage)
            => storageRepository.AddStorage(storage);

        public StorageDto DeleteStorage([Service] IStorageRepository storageRepository, int productId)
            => storageRepository.DeleteStorage(productId);

    }
}
