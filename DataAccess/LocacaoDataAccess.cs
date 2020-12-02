using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class LocacaoDataAccess : BaseDataAccess<LocacaoDataAccess>
    {
        public List<LocacaoEntity> Listar(out string mensagemErro)
        {
            string json = Listar("Locacao", out mensagemErro);

            List<LocacaoEntity> listLocacoes = (List<LocacaoEntity>)JsonConvert.DeserializeObject(json, typeof(List<LocacaoEntity>));

            return listLocacoes;
        }

        public LocacaoEntity Obter(long id, out string mensagemErro)
        {
            mensagemErro = "";

            string json = Listar("Locacao", out mensagemErro);

            List<LocacaoEntity> listLocacoes = (List<LocacaoEntity>)JsonConvert.DeserializeObject(json, typeof(List<LocacaoEntity>));

            var locacao = listLocacoes.Where(p => p.IdLocacao == id).FirstOrDefault();

            if(locacao == null)
            {
                mensagemErro = "Locacão não encontrada";
                return null;
            }

            return locacao;
        }

        public bool ExcluirLocacao(long IdLocacao, out string mensagemErro)
        {
            mensagemErro = "";

            string json = Listar("Locacao", out mensagemErro);

            List<LocacaoEntity> listLocacoes = (List<LocacaoEntity>)JsonConvert.DeserializeObject(json, typeof(List<LocacaoEntity>));

            var locacao = listLocacoes.FirstOrDefault(p => p.IdLocacao == IdLocacao);
            
            if(locacao == null)
            {
                mensagemErro = "Locação não encontrada na base de dados.";
                return false;
            }

            listLocacoes.FirstOrDefault(p => p.IdLocacao == IdLocacao).Ativo = false;

            return Salvar("Locacao", JsonConvert.SerializeObject(listLocacoes), out mensagemErro);
        }

        public bool SalvarLocacao(LocacaoEntity locacaoEntity, out string mensagemErro)
        {
            mensagemErro = "";

            string json = Listar("Locacao", out mensagemErro);

            List<LocacaoEntity> listLocacoes = (List<LocacaoEntity>)JsonConvert.DeserializeObject(json, typeof(List<LocacaoEntity>));

            var locacao = listLocacoes.FirstOrDefault(p => p.IdLocacao == locacaoEntity.IdLocacao);

            if (locacaoEntity.IdLocacao != null && locacao == null)
            {
                mensagemErro = "Locação não encontrada na base de dados.";
                return false;
            }

            if (locacaoEntity.IdLocacao == null)
            {
                locacaoEntity.IdLocacao = listLocacoes.Max(p => p.IdLocacao) + 1;
            }

            listLocacoes.Remove(locacao);

            locacao = locacaoEntity;

            listLocacoes.Add(locacao);

            listLocacoes = listLocacoes.OrderBy(p => p.IdLocacao).ToList();

            return Salvar("Locacao", JsonConvert.SerializeObject(listLocacoes), out mensagemErro);
        }


    }
}