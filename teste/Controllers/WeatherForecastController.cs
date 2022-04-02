using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using teste.Interface;
using teste.Servicos;

namespace teste.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly Integrador integrador;

        public WeatherForecastController(Integrador integrador)
        {
            this.integrador = integrador;
        }

        [HttpGet]
        public IActionResult Get(ETipo tipo)
        {
            return Ok(integrador.Integrar(tipo));

        }
    }
}
