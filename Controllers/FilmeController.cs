using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApplication1.BusinessLogic;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class FilmeController : ApiController
    {
        FilmeBusinessLogic busFilme = new FilmeBusinessLogic();

        [Route("api/filme/ListarFilmes")]
        [HttpGet]
        public IEnumerable<FilmeEntity> ListarFilmes()
        {
            string mensagemErro;
            var filmes = busFilme.Listar(out mensagemErro);

            if (string.IsNullOrWhiteSpace(mensagemErro))
                return filmes.ToList();

            filmes = new List<FilmeEntity>();
            filmes.Add(new FilmeEntity() { MensagemErro = mensagemErro });

            return filmes.ToList();
        }

        [Route("api/filme/ObterFilme/{id}")]
        [HttpGet]

        public FilmeEntity ObterFilme(long id)
        {
            string mensagemErro;
            return busFilme.Obter(id, out mensagemErro);
        }

        [Route("api/filme/ExcluirFilme/{id}")]
        [HttpGet]
        public string ExcluirFilme(long id)
        {
            string mensagemErro;

            var toReturn = busFilme.ExcluirFilme(id, out mensagemErro);
            if (string.IsNullOrWhiteSpace(mensagemErro))
                return "Sucesso";

            return mensagemErro;
        }

        [HttpGet]
        [Route("api/filme/SalvarFilme/{id}/{nome}/{diarias}/{disponivel}/{ativo}")]
        public string SalvarFilme(long? id, string nome, int diarias, bool disponivel, bool ativo)
        {
            string mensagemErro;

            var filmeEntity = new FilmeEntity()
            {
                IdFilme = id,
                Nome = nome,
                Diarias = diarias,
                Disponivel = disponivel,
                Ativo = ativo
            };

            var toReturn = busFilme.SalvarFilme(filmeEntity, out mensagemErro);
            if (string.IsNullOrWhiteSpace(mensagemErro))
                return $"Sucesso. Id {filmeEntity.IdFilme}";

            return mensagemErro;
        }
    }
}