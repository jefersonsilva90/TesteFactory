using System;
using teste.Interface;

namespace teste.Servicos
{
    public class ServicoA: IService
    {

        public string Servico()
        {
            return "Eu sou o padrão A";
        }

        public ETipo GetTipo()
        {
            return ETipo.PadraoA;
        }
    }
}
