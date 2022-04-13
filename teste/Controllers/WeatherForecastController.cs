using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using teste.Servicos;

namespace teste.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly Integrador integrador;
        private readonly IConfiguration _configutation;
        private readonly IHttpClientFactory _clientFactory;

        public WeatherForecastController(Integrador integrador, IConfiguration configutation, IHttpClientFactory client)
        {
            this.integrador = integrador;
            _configutation = configutation;
            _clientFactory = client;
        }

        [HttpGet]
        public IActionResult Get(ETipo tipo)
        {
            return Ok(integrador.Integrar(tipo));

        }
        
        [HttpPost]
        public async Task<IActionResult> Token()
        {
            var httpClient = _clientFactory.CreateClient("keycloack");

            var corpo = new CorpoDto();
            corpo.client_id = "myclient";
            corpo.client_secret = "ZHnJaZORacHZrvimIcYCCtna47RClRDA";
            corpo.grant_type = "password";
            corpo.password = "090490";
            corpo.username = "myuser";

            var body = new[]
            {
                new KeyValuePair<string, string>("client_id", "myclient"),
                new KeyValuePair<string, string>("client_secret", "ZHnJaZORacHZrvimIcYCCtna47RClRDA"),
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("password", "090490"),
                new KeyValuePair<string, string>("username", "myuser"),
            };

            var result = await httpClient.PostAsync("protocol/openid-connect/token", new FormUrlEncodedContent(body));
            var responseBody = await result.Content.ReadAsStringAsync();
            return Ok(responseBody);
        }

        [HttpPost] 
        [Route("Votacao")]
         public async Task<ActionResult> Votacao()
         {
             var responseBody = "";
             int i = 0;
             while (i < 100)
             {
                 var httpClient = _clientFactory.CreateClient("votacao");
                 var bodyPay = new BodyVotacao
                 {
                     poll_id = 110837,
                     ans_id = "373791",
                     origem = "",
                     browser = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.75 Safari/537.36"
                 };
                 string body = JsonConvert.SerializeObject(bodyPay);
                 var result = await httpClient.PostAsync( "vote", new  StringContent(body));
                 responseBody = await result.Content.ReadAsStringAsync();
                 Console.WriteLine(responseBody);
                 await Task.Delay(1000);

                if (i == 998)
                {
                    i = 0;
                    await Task.Delay(50000);
                }

                i++;
             }
  
             return Ok(responseBody);
         }

    }

    public class BodyVotacao
    {
        public int poll_id { get; set; }
        public string ans_id { get; set; }
        public string origem { get; set; }
        public string browser { get; set; }
    }
    public class CorpoDto
    {
        public string client_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string grant_type { get; set; }
        public string client_secret { get; set; }
    }
}
