using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class ClienteDataAccess : BaseDataAccess<ClienteDataAccess>
    {

        public List<ClienteEntity> Listar(out string mensagemErro)
        {
            string json = Listar("Cliente", out mensagemErro);

            if (json == null)
            {
                mensagemErro = "Nenhum Registro Encontrado";
                return new List<ClienteEntity>();
            }
            List<ClienteEntity> listCliente = (List<ClienteEntity>)JsonConvert.DeserializeObject(json, typeof(List<ClienteEntity>));
            listCliente = listCliente.Where(p => p.Ativo).ToList();

            return listCliente;
        }

        public ClienteEntity Obter(long id, out string mensagemErro)
        {
            mensagemErro = "";

            string json = Listar("Cliente", out mensagemErro);

            List<ClienteEntity> listCliente = (List<ClienteEntity>)JsonConvert.DeserializeObject(json, typeof(List<ClienteEntity>));

            var cliente = listCliente.Where(p => p.IdCliente == id).FirstOrDefault();

            if (cliente == null)
            {
                mensagemErro = "Filme não encontrado";
                return null;
            }

            return cliente;
        }

        public bool ExcluirCliente(long IdCliente, out string mensagemErro)
        {
            mensagemErro = "";

            string json = Listar("Cliente", out mensagemErro);

            List<ClienteEntity> listCliente = (List<ClienteEntity>)JsonConvert.DeserializeObject(json, typeof(List<ClienteEntity>));

            var filme = listCliente.FirstOrDefault(p => p.IdCliente == IdCliente);

            if (filme == null)
            {
                mensagemErro = "Cliente não encontrado na base de dados.";
                return false;
            }

            listCliente.FirstOrDefault(p => p.IdCliente == IdCliente).Ativo = false;

            return Salvar("Cliente", JsonConvert.SerializeObject(listCliente), out mensagemErro);
        }

        public bool SalvarCliente(ClienteEntity clienteEntity, out string mensagemErro)
        {
            mensagemErro = "";

            string json = Listar("Cliente", out mensagemErro);

            List<ClienteEntity> listCliente = (List<ClienteEntity>)JsonConvert.DeserializeObject(json, typeof(List<ClienteEntity>));

            var cliente = listCliente.FirstOrDefault(p => p.IdCliente == clienteEntity.IdCliente);

            if (clienteEntity.IdCliente != null && cliente == null)
            {
                mensagemErro = "Cliente não encontrada na base de dados.";
                return false;
            }

            if (cliente == null)
            {
                clienteEntity.IdCliente = listCliente.Max(p => p.IdCliente) + 1;
            }

            listCliente.Remove(cliente);

            cliente = clienteEntity;

            listCliente.Add(cliente);

            listCliente = listCliente.OrderBy(p => p.IdCliente).ToList();

            return Salvar("Cliente", JsonConvert.SerializeObject(listCliente), out mensagemErro);
        }


    }
}