using System;
using teste.Interface;

namespace teste.Servicos
{
    public class ServicoB :  IService
    {

        public string Servico()
        {
            return "Eu sou o padrão B";
        }

        public  ETipo GetTipo()
        {
            return ETipo.PadraoB;
        }
    }
}
