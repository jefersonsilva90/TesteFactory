using System;
using System.Collections.Generic;
using teste.Interface;
using System.Linq;

namespace teste.Servicos
{
    public class Integrador
    {
        private IService servico;
        private readonly IEnumerable<IService> listaServico;

        public Integrador()
        {
        }

        public Integrador(IEnumerable<IService> service)
        {
            this.listaServico = service;
        }

        public string Integrar(ETipo tipo)
        {
            servico = listaServico.Where(x => x.GetTipo() == tipo).FirstOrDefault();

            return servico.Servico().ToString();
        }
    }
}
