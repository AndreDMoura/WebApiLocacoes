using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApplication1.BusinessLogic;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ClienteController : ApiController
    {
        ClienteBusinessLogic busCliente = new ClienteBusinessLogic();

        [Route("api/cliente/ListarClientes")]
        [HttpGet]
        public IEnumerable<ClienteEntity> ListarClientes()
        {
            string mensagemErro;
            var clientes = busCliente.Listar(out mensagemErro);

            if (string.IsNullOrWhiteSpace(mensagemErro))
                return clientes.ToList();

            clientes = new List<ClienteEntity>();
            clientes.Add(new ClienteEntity() { MensagemErro = mensagemErro });
            
            return clientes.ToList();
        }

        [Route("api/cliente/ObterCliente/{id}")]
        [HttpGet]
        
        public ClienteEntity ObterCliente(long id)
        {
            string mensagemErro;
            return busCliente.Obter(id, out mensagemErro);
        }

        [Route("api/cliente/ExcluirCliente/{id}")]
        [HttpGet]
        public string ExcluirCliente(long id)
        {
            string mensagemErro;

            var toReturn = busCliente.ExcluirCliente(id, out mensagemErro);
            if (string.IsNullOrWhiteSpace(mensagemErro))
                return "Sucesso";

            return mensagemErro;
        }

        [HttpGet]
        [Route("api/cliente/SalvarCliente/{id}/{nome}/{ativo}")]
        public string SalvarCliente(long? id, string nome, bool ativo)
        {
            string mensagemErro;

            var clienteEntity = new ClienteEntity()
            {
                IdCliente = id,
                Nome = nome,
                Ativo = ativo
            };

            var toReturn = busCliente.SalvarCliente(clienteEntity, out mensagemErro);
            if (string.IsNullOrWhiteSpace(mensagemErro))
                return $"Sucesso. Id {clienteEntity.IdCliente}";

            return mensagemErro;
        }

    }
}