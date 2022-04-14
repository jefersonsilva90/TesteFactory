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
using teste.Dto;
using teste.Servicos;

namespace teste.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly Integrador integrador;
        private readonly VotacaoService _votacaoService;
        private readonly TokenService _tokenService;

        public WeatherForecastController(Integrador integrador, VotacaoService votacaoService, TokenService tokenService)
        {
            this.integrador = integrador;
            _votacaoService = votacaoService;
            _tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult Get(ETipo tipo)
        {
            return Ok(integrador.Integrar(tipo));
        }
        
        [HttpPost]
        public async Task<IActionResult> Token()
        {
            var token = _tokenService.GeraToken();
            return Ok(token);
        }

        [HttpPost]
        [Route("Votacao")]
        public async Task<ActionResult> Votacao()
        {
            var retorno = await _votacaoService.Votar();
            return Ok(retorno);
        }

    }
}
