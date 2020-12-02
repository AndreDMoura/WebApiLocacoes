using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using System.Web.Caching;
using System.IO;

namespace WebApplication1.DataAccess
{
    public class BaseDataAccess<T>
    {
        string Diretorio = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);

        public void CarregarInicio(out string mensagemErro)
        {
            mensagemErro = "";

            CarregarClienteInicio(out mensagemErro);
            
            if (!string.IsNullOrWhiteSpace(mensagemErro))
                return;

            CarregarFilmeInicio(out mensagemErro);

            if (!string.IsNullOrWhiteSpace(mensagemErro))
                return;

            CarregarLocacoesInicio(out mensagemErro);
        }

        private void CarregarClienteInicio(out string mensagemErro)
        {
            var listCliente = new List<ClienteEntity>();
            for(int i = 1; i <= 10; i++)
            {
                var cliente = new ClienteEntity();
                
                cliente.IdCliente = i;
                cliente.Nome = $"Cliente {i}";
                cliente.Ativo = true;

                listCliente.Add(cliente);
            }

            Salvar("Cliente", JsonConvert.SerializeObject(listCliente), out mensagemErro);
        }

        private void CarregarFilmeInicio(out string mensagemErro)
        {
            var listFilme = new List<FilmeEntity>();
            Random rnd = new Random();
            for (int i = 1; i <= 5; i++)
            {
                var filme = new FilmeEntity();

                filme.IdFilme = i;
                filme.Nome = $"Filme {i}";
                filme.Diarias = rnd.Next(1, 3);
                filme.Disponivel = true;
                filme.Ativo = true;

                listFilme.Add(filme);
            }

            Salvar("Filme", JsonConvert.SerializeObject(listFilme), out mensagemErro);
        }

        public void CarregarLocacoesInicio(out string mensagemErro)
        {
            var listLocacoes = new List<LocacaoEntity>();
            Random rnd = new Random();
            for (int i = 1; i <= 5; i++)
            {
                var locacoes = new LocacaoEntity();

                locacoes.IdLocacao = listLocacoes.Any() ? listLocacoes.Max(p => p.IdLocacao) + 1 : i;
                locacoes.IdCliente = rnd.Next(1, 10);
                locacoes.IdFilme = rnd.Next(1, 10);
                locacoes.DtLocacao = DateTime.Today;
                locacoes.DtDevolucao = null;
                locacoes.Atraso = null;
                locacoes.Ativo = true;

                if (listLocacoes.Any(p => p.IdCliente == locacoes.IdCliente
                                          && p.IdFilme == locacoes.IdFilme))
                    continue;

                listLocacoes.Add(locacoes);
            }

            Salvar("Locacao", JsonConvert.SerializeObject(listLocacoes), out mensagemErro);

            FilmeDataAccess filmeDataAccess = new FilmeDataAccess();

            var filmes = filmeDataAccess.Listar(out mensagemErro);

            if (!string.IsNullOrWhiteSpace(mensagemErro))
                return;

            var auxFilmes = filmes.Where(p => listLocacoes.Select(loc => loc.IdFilme).Contains(p.IdFilme)).ToList();
            
            foreach (var filme in auxFilmes)
            {
                filmes.Remove(filme);

                filme.Disponivel = false;

                filmes.Add(filme);
            }

            filmes = filmes.OrderBy(p => p.IdFilme).ToList();

            Salvar("Filmes", JsonConvert.SerializeObject(listLocacoes), out mensagemErro);
        }

        public bool Salvar(string item, string jsonString, out string mensagemErro)
        {
            mensagemErro = "";
            try
            {
                string arquivo = Path.Combine(Diretorio, $"{item}.txt");

                Directory.CreateDirectory(Diretorio);

                File.Create(arquivo).Dispose();

                File.WriteAllText(arquivo, jsonString);
            }
            catch (Exception ex)
            {
                mensagemErro = ex.Message;
                return false;
            }

            return true;
        }

        public string Listar(string item, out string mensagemErro)
        {
            mensagemErro = "";

            try
            {
                return File.ReadAllText(Path.Combine(Diretorio, $"{item}.txt"));
            }
            catch (Exception ex)
            {
                mensagemErro = ex.Message;
                return null;
            }
        }


    }

}