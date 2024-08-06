using Commerce.ShoppingAPI.Models.Dto;

namespace Commerce.ShoppingAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
