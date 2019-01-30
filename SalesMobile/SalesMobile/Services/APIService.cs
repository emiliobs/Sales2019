namespace SalesMobile.Services
{
    using Newtonsoft.Json;
    using SalesCommon;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class APIService
    {
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
