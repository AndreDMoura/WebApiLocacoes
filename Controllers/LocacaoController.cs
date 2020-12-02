using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApplication1.BusinessLogic;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LocacaoController : ApiController
    {
        LocacaoBusinessLogic busLocacao = new LocacaoBusinessLogic();

        [Route("api/locacao/ListarLocacoes")]
        [HttpGet]
        public IEnumerable<LocacaoEntity> ListarLocacoes()
        {
            string mensagemErro;
            var locacoes = busLocacao.Listar(out mensagemErro);

            if (string.IsNullOrWhiteSpace(mensagemErro))
                return locacoes.ToList();

            locacoes = new List<LocacaoEntity>();
            locacoes.Add(new LocacaoEntity() { MensagemErro = mensagemErro });

            return locacoes.ToList();
        }

        [Route("api/locacao/ObterLocacao/{id}")]
        [HttpGet]

        public LocacaoEntity ObterLocacao(long id)
        {
            string mensagemErro;
            return busLocacao.Obter(id, out mensagemErro);
        }

        [Route("api/locacao/ExcluirLocacao/{id}")]
        [HttpGet]
        public string ExcluirLocacao(long id)
        {
            string mensagemErro;

            var toReturn = busLocacao.ExcluirLocacao(id, out mensagemErro);
            if (string.IsNullOrWhiteSpace(mensagemErro))
                return "Sucesso";

            return mensagemErro;
        }

        [HttpGet]
        [Route("api/locacao/SalvarLocacao/{id}/{idcliente}/{idfilme}/{dtlocacao}/{dtdevolucao}/{ativo}")]
        public string SalvarLocacao(long? id, long? idCliente, long? idFilme, string dtLocacao, string dtDevolucao, bool ativo)
        {
            string mensagemErro;

            DateTime? auxDtDevolucao = null;
            if(!string.IsNullOrWhiteSpace(dtDevolucao) && dtDevolucao != "null")
                auxDtDevolucao = DateTime.ParseExact(dtDevolucao, "ddMMyyyy", CultureInfo.InvariantCulture);
            
            var locacaoEntity = new LocacaoEntity()
            {
                IdLocacao = id,
                IdCliente = idCliente,
                IdFilme = idFilme,
                DtLocacao = DateTime.ParseExact(dtLocacao, "ddMMyyyy", CultureInfo.InvariantCulture),
                DtDevolucao = auxDtDevolucao,
                Ativo = ativo
            };

            var toReturn = busLocacao.SalvarLocacao(locacaoEntity, out mensagemErro);
            if (string.IsNullOrWhiteSpace(mensagemErro))
                return $"Sucesso. Id {locacaoEntity.IdLocacao}";

            return mensagemErro;
        }

        [HttpGet]
        [Route("api/locacao/Alugar/{idcliente}/{idfilme}")]
        public string Alugar(long idCliente, long idFilme)
        {
            string mensagemErro;
            long? IdLocacao;

            var toReturn = busLocacao.Alugar(idCliente, idFilme, out mensagemErro, out IdLocacao);
            if (string.IsNullOrWhiteSpace(mensagemErro))
                return $"Sucesso. Id {IdLocacao}";

            return mensagemErro;
        }

        [HttpGet]
        [Route("api/locacao/Devolver/{idlocacao}")]
        public string Devolver(long IdLocacao)
        {
            string mensagemErro;

            if(busLocacao.Devolver(IdLocacao, out mensagemErro))
                return $"Sucesso";

            return mensagemErro;
        }


    }
}