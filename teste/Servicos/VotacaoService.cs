using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using teste.Dto;

namespace teste.Servicos
{
    public class VotacaoService
    {
        private readonly IHttpClientFactory _clientFactory;

        public VotacaoService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<string> Votar()
        {
            var responseBody = "";
            int i = 0;
            int total = 0;
            while (i < 1000)
            {
                var httpClient = _clientFactory.CreateClient("votacao");
                var bodyPay = new BodyVotacao
                {
                    poll_id = 110837,
                    ans_id = "373791",
                    origem = "",
                    browser =
                        "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.75 Safari/537.36"
                };
                string body = JsonConvert.SerializeObject(bodyPay);
                var result = await httpClient.PostAsync("vote", new StringContent(body));
                responseBody = await result.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody + "Total Votos: " + total);
                await Task.Delay(1000);

                if (i == 998)
                {
                    i = 0;
                    await Task.Delay(50000);
                }

                i++;
                total++;
            }

            return responseBody + "Total Votos: " + total;
        }
    }
}