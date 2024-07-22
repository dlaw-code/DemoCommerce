//using Coupon.Web.Models;
//using Coupon.Web.Service.IService;

//using Microsoft.AspNetCore.Mvc.ApiExplorer;
//using Newtonsoft.Json;
//using System.Net;
//using System.Net.Http;
//using System.Text;
//using System.Text.Json;
//using static Coupon.Web.Utility.SD;

//namespace Coupon.Web.Service
//{
//    public class BaseService : IBaseService
//    {
//        private readonly IHttpClientFactory _httpClientFactory;
//        public BaseService(IHttpClientFactory httpClientFactory)
//        {
//            _httpClientFactory = httpClientFactory;
//        }

//        public async Task<ResponseDto<T>?> SendAsync<T>(RequestDto requestDto)
//        {

//            HttpClient client = _httpClientFactory.CreateClient("CouponAPI");
//            HttpRequestMessage message = new();
//            message.Headers.Add("Accept", "application/json");

//            message.RequestUri = new Uri(requestDto.Url);
//            if (requestDto.Data != null)
//            {
//                message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
//            }

//            HttpResponseMessage? apiResponse = null;

//            switch (requestDto.ApiType)
//            {
//                case ApiType.POST:
//                    message.Method = HttpMethod.Post;
//                    break;
//                case ApiType.DELETE:
//                    message.Method = HttpMethod.Delete;
//                    break;
//                case ApiType.PUT:
//                    message.Method = HttpMethod.Put;
//                    break;
//                default:
//                    message.Method = HttpMethod.Get;
//                    break;
//            }

//            apiResponse = await client.SendAsync(message);

//            ResponseDto<T> response = new();

//            switch (apiResponse.StatusCode)
//            {
//                case HttpStatusCode.NotFound:
//                    response.IsSuccess = false;
//                    response.DisplayMessage = "Not Found";
//                    break;
//                case HttpStatusCode.Forbidden:
//                    response.IsSuccess = false;
//                    response.DisplayMessage = "Access Denied";
//                    break;
//                case HttpStatusCode.Unauthorized:
//                    response.IsSuccess = false;
//                    response.DisplayMessage = "Unauthorized";
//                    break;
//                case HttpStatusCode.InternalServerError:
//                    response.IsSuccess = false;
//                    response.DisplayMessage = "Internal Server Error";
//                    break;
//                default:
//                    var apiContent = await apiResponse.Content.ReadAsStringAsync();
//                    response.Result = JsonConvert.DeserializeObject<T>(apiContent);
//                    break;
//            }

//            return response;
//        }
//    }
//}
