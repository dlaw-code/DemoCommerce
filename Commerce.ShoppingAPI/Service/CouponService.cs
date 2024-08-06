using Commerce.ShoppingAPI.Models.Dto;
using Commerce.ShoppingAPI.Models.ResponseDto;
using Commerce.ShoppingAPI.Service.IService;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Commerce.ShoppingAPI.Service
{
    public class CouponService : ICouponService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CouponService(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }

        public async Task<CouponDto> GetCoupon(string couponCode)
        {
            var client = _httpClientFactory.CreateClient("Coupon");
            var response = await client.GetAsync($"/api/coupon/GetByCode/{couponCode}");

            if (response.IsSuccessStatusCode)
            {
                var apiContent = await response.Content.ReadAsStringAsync();
                var resp = JsonConvert.DeserializeObject<ResponseDto<CouponDto>>(apiContent);

                if (resp?.IsSuccess == true)
                {
                    return resp.Result;
                }
            }

            return new CouponDto();
        }
    }
}
