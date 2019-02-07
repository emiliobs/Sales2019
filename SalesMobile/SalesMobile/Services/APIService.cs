namespace SalesMobile.Services
{
    using Newtonsoft.Json;
    using Plugin.Connectivity;
    using SalesCommon;
    using SalesMobile.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
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

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable(Languages.google_com);
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
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}";
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response()
                    {
                        IsSuccess = true,
                        Message = result,

                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(result);

                return  new Response()
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
    }
}
