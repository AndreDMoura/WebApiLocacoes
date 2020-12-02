using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.DataAccess;
using WebApplication1.Models;

namespace WebApplication1.BusinessLogic
{
    public class ClienteBusinessLogic
    {
        ClienteDataAccess dataAccessCliente = new ClienteDataAccess();

        public List<ClienteEntity> Listar(out string mensagemErro)
        {
            var toReturn = dataAccessCliente.Listar(out mensagemErro);

            if(string.IsNullOrWhiteSpace(mensagemErro))
            {
                return toReturn;
            }

            return null;
        }
        public ClienteEntity Obter(long id, out string mensagemErro)
        {
            var toReturn = dataAccessCliente.Obter(id, out mensagemErro);

            if (string.IsNullOrWhiteSpace(mensagemErro))
            {
                return toReturn;
            }

            return null;
        }


        public bool ExcluirCliente(long IdCliente, out string mensagemErro)
        {
            return dataAccessCliente.ExcluirCliente(IdCliente, out mensagemErro);
        }

        public bool SalvarCliente(ClienteEntity clienteEntity, out string mensagemErro)
        {
            mensagemErro = "";

            if(clienteEntity.IdCliente != null)
            {
                ValidarCliente(clienteEntity, out mensagemErro);
            }

            if (string.IsNullOrWhiteSpace(mensagemErro))
            {
                return dataAccessCliente.SalvarCliente(clienteEntity, out mensagemErro);
            }

            return false;

        }

        private bool ValidarCliente(ClienteEntity cliente, out string mensagemErro)
        {
            var clientes = Listar(out mensagemErro);

            if(string.IsNullOrWhiteSpace(mensagemErro))
            {
                if (clientes.Any(p =>
                                    p.Nome == cliente.Nome
                                    && p.IdCliente != cliente.IdCliente))
                {
                    mensagemErro = "Cliente já existente na base.";
                    return false;
                }

            }

            return true;
        }

    }
}