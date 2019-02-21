namespace SalesMobile.Services
{
    using Newtonsoft.Json;
    using Plugin.Connectivity;
    using SalesCommon;
    using SalesMobile.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public class APIService
    {

        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response()
                {
                    IsSuccess = false,
                    Message = Languages.TurnOnInternet,
                };

            }

            bool isReachable = await CrossConnectivity.Current.IsRemoteReachable(Languages.google_com);
            if (!isReachable)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Languages.NoInternet,
                };
            }

            return new Response
            {
                IsSuccess = true,
                Message = Languages.Ok,
            };
        }
        public async Task<Response> GetList<T>(string urlBase, string prefix, string controller)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };
                string url = $"{prefix}{controller}";
                HttpResponseMessage response = await client.GetAsync(url);
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response()
                    {
                        IsSuccess = true,
                        Message = result,

                    };
                }

                List<T> list = JsonConvert.DeserializeObject<List<T>>(result);

                return new Response()
                {
                    IsSuccess = true,
                    Result = list,
                };

            }
            catch (Exception ex)
            {

                return new Response()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }

        }
        public async Task<Response> Post<T>(string urlBase, string prefix, string controller, T model)
        {
            try
            {
                string request = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(request, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };
                string url = $"{prefix}{controller}";
                HttpResponseMessage response = await client.PostAsync(url, content);
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response()
                    {
                        IsSuccess = true,
                        Message = result,

                    };
                }

                T obj = JsonConvert.DeserializeObject<T>(result);

                return new Response()
                {
                    IsSuccess = true,
                    Result = obj,
                };

            }
            catch (Exception ex)
            {

                return new Response()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }

        }
    }
}
