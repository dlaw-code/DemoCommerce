using Commerce.Web.Models;
using Coupon.Web.Models;
using Coupon.Web.Service.IService;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using static Coupon.Web.Utility.SD;

namespace Coupon.Web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseDto<T>> SendAsync<T>(RequestDto requestDto)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("CommerceAPI");
                HttpRequestMessage message = new HttpRequestMessage
                {
                    Method = new HttpMethod(requestDto.ApiType.ToString()),
                    RequestUri = new Uri(requestDto.Url),
                    Content = requestDto.Data != null
                        ? new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json")
                        : null
                };
                message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage apiResponse = await client.SendAsync(message);
                var content = await apiResponse.Content.ReadAsStringAsync();

                ResponseDto<T> responseDto;

                try
                {
                    responseDto = JsonConvert.DeserializeObject<ResponseDto<T>>(content);
                }
                catch (JsonSerializationException)
                {
                    // Handle unexpected response format
                    var data = JsonConvert.DeserializeObject<T>(content);
                    responseDto = new ResponseDto<T>
                    {
                        Result = data,
                        IsSuccess = apiResponse.IsSuccessStatusCode,
                        Message = apiResponse.IsSuccessStatusCode ? "Success" : "Error"
                    };
                }

                return responseDto;
            }
            catch (Exception ex)
            {
                return new ResponseDto<T>
                {
                    Message = ex.Message,
                    IsSuccess = false
                };
            }
        }




    }
}


