using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using teste.Dto;

namespace teste.Servicos
{
    public class TokenService
    {
        private readonly IHttpClientFactory _clientFactory;

        public TokenService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<string> GeraToken()
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
            return responseBody;
        }
    }
}