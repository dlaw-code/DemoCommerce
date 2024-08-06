using Commerce.ShoppingAPI.Models.Dto;
using Commerce.ShoppingAPI.Service.IService;
using Newtonsoft.Json;

namespace Commerce.ShoppingAPI.Service
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var client = _httpClientFactory.CreateClient("Product");
            var response = await client.GetAsync("/api/product");
            var apiContent = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDto<IEnumerable<ProductDto>>>(apiContent);
            if (resp.IsSuccess)
            {
                return resp.Result;
            }

            return new List<ProductDto>();
        }
    }
}
