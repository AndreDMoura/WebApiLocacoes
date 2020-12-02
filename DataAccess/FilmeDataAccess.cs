using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class FilmeDataAccess : BaseDataAccess<FilmeDataAccess>
    {
        public List<FilmeEntity> Listar(out string mensagemErro)
        {
            mensagemErro = "";

            string json = Listar("Filme", out mensagemErro);

            List<FilmeEntity> filmes = (List<FilmeEntity>)JsonConvert.DeserializeObject(json, typeof(List<FilmeEntity>));

            return filmes;
        }
        public FilmeEntity Obter(long id, out string mensagemErro)
        {
            mensagemErro = "";

            string json = Listar("Filme", out mensagemErro);

            List<FilmeEntity> listFilme = (List<FilmeEntity>)JsonConvert.DeserializeObject(json, typeof(List<FilmeEntity>));

            var filme = listFilme.Where(p => p.IdFilme == id).FirstOrDefault();

            if (filme == null)
            {
                mensagemErro = "Filme não encontrado";
                return null;
            }

            return filme;
        }

        public bool ExcluirFilme(long IdFilme, out string mensagemErro)
        {
            mensagemErro = "";

            string json = Listar("Filme", out mensagemErro);

            List<FilmeEntity> listFilme = (List<FilmeEntity>)JsonConvert.DeserializeObject(json, typeof(List<FilmeEntity>));

            var filme = listFilme.FirstOrDefault(p => p.IdFilme == IdFilme);

            if (filme == null)
            {
                mensagemErro = "Filme não encontrado na base de dados.";
                return false;
            }

            listFilme.FirstOrDefault(p => p.IdFilme == IdFilme).Ativo = false;

            return Salvar("Filme", JsonConvert.SerializeObject(listFilme), out mensagemErro);
        }

        public bool SalvarFilme(FilmeEntity filmeEntity, out string mensagemErro)
        {
            mensagemErro = "";

            string json = Listar("Filme", out mensagemErro);

            List<FilmeEntity> listFilme = (List<FilmeEntity>)JsonConvert.DeserializeObject(json, typeof(List<FilmeEntity>));

            var filme = listFilme.FirstOrDefault(p => p.IdFilme == filmeEntity.IdFilme);

            if (filmeEntity.IdFilme != null && filme == null)
            {
                mensagemErro = "Filme não encontrado na base de dados.";
                return false;
            }

            if (filme == null)
            {
                filmeEntity.IdFilme = listFilme.Max(p => p.IdFilme) + 1;
            }

            listFilme.Remove(filme);

            filme = filmeEntity;

            listFilme.Add(filme);

            listFilme = listFilme.OrderBy(p => p.IdFilme).ToList();

            return Salvar("Filme", JsonConvert.SerializeObject(listFilme), out mensagemErro);
        }

    }
}