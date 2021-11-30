using BusinessLogic.Entities;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Clients.PaymentIntegrationClient
{
    public class Client : IClient
    {
        private PaymentIntegration _paymentIntegration;

        public Client()
        {

        }

        public void ConfigureSettings(PaymentIntegration paymentIntegration)
        {
            _paymentIntegration = paymentIntegration;
        }

        public async Task<RefundResponse> RequestRefund(RefundRequest refundRequest)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(_paymentIntegration.BaseUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpContent content = new StringContent(JsonConvert.SerializeObject(refundRequest), Encoding.UTF8, "application/json");

                HttpResponseMessage result = await client.PostAsync("api/Refund", content);

                if (result.IsSuccessStatusCode)
                {
                    var response = result.Content.ReadAsStringAsync().Result;

                    return JsonConvert.DeserializeObject<RefundResponse>(response);
                }
            }

            return null;
        }
    }
}
