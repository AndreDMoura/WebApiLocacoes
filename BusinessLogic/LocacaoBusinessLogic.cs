using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.DataAccess;
using WebApplication1.Models;

namespace WebApplication1.BusinessLogic
{
    public class LocacaoBusinessLogic
    {
        LocacaoDataAccess dataAccessLocacao = new LocacaoDataAccess();

        public List<LocacaoEntity> Listar(out string mensagemErro)
        {
            var toReturn = dataAccessLocacao.Listar(out mensagemErro);

            if (string.IsNullOrWhiteSpace(mensagemErro))
            {
                return toReturn;
            }

            return null;
        }

        public LocacaoEntity Obter(long IdLocacao, out string mensagemErro)
        {
            var toReturn = dataAccessLocacao.Obter(IdLocacao, out mensagemErro);

            if (string.IsNullOrWhiteSpace(mensagemErro))
            {
                return toReturn;
            }

            return null;
        }

        public bool ExcluirLocacao(long IdLocacao, out string mensagemErro)
        {
            return dataAccessLocacao.ExcluirLocacao(IdLocacao, out mensagemErro);
        }

        public bool SalvarLocacao(LocacaoEntity locacaoEntity, out string mensagemErro)
        {
            return dataAccessLocacao.SalvarLocacao(locacaoEntity, out mensagemErro);
        }

        public bool Alugar(long IdCliente, long IdFilme, out string mensagemErro, out long? IdLocacao)
        {
            var busFilme = new FilmeBusinessLogic();
            var filme = busFilme.Obter(IdFilme, out mensagemErro);
            IdLocacao = null;

            if (!ValidacoesAlugar(filme, ref mensagemErro))
                return false;
            
            filme.Disponivel = false;

            var Locacao = new LocacaoEntity()
            {
                IdLocacao = null,
                IdCliente = IdCliente,
                IdFilme = IdFilme,
                DtLocacao = DateTime.Today,
                Ativo = true
            };


            busFilme.SalvarFilme(filme, out mensagemErro);
            if (!string.IsNullOrWhiteSpace(mensagemErro))
                return false;

            dataAccessLocacao.SalvarLocacao(Locacao, out mensagemErro);

            if (!string.IsNullOrWhiteSpace(mensagemErro))
            {
                filme.Disponivel = true;
                busFilme.SalvarFilme(filme, out mensagemErro);

                mensagemErro = "Erro ao salvar Locação";
                return false;
            }

            IdLocacao = Locacao.IdLocacao;

            //Sucesso
            return true;
        }

        private bool ValidacoesAlugar(FilmeEntity filme, ref string mensagemErro)
        {
            if (!string.IsNullOrWhiteSpace(mensagemErro))
                return false;

            if (!filme.EstaDisponivel())
            {
                mensagemErro = "Filme não está disponível!";
                return false;
            }

            //Sucesso
            return true;
        }

        public bool Devolver(long IdLocacao, out string mensagemErro)
        {
            var busFilme = new FilmeBusinessLogic();
            var locacao = Obter(IdLocacao, out mensagemErro);
            
            if (!string.IsNullOrWhiteSpace(mensagemErro))
                return false;

            var filme = busFilme.Obter((long)locacao.IdFilme, out mensagemErro);

            if (!string.IsNullOrWhiteSpace(mensagemErro))
                return false;

            locacao.DtDevolucao = DateTime.Today;
            
            if (SubtrairData((DateTime)locacao.DtDevolucao, locacao.DtLocacao) > filme.Diarias)
                locacao.Atraso = true;
            else
                locacao.Atraso = false;

            
            SalvarLocacao(locacao, out mensagemErro);

            if (!string.IsNullOrWhiteSpace(mensagemErro))
                return false;

            filme.Disponivel = true;
            busFilme.SalvarFilme(filme, out mensagemErro);

            if (!string.IsNullOrWhiteSpace(mensagemErro))
            {
                locacao.DtDevolucao = null;
                locacao.Atraso = null;

                SalvarLocacao(locacao, out mensagemErro);

                mensagemErro = "Não foi possível salvar a devolução!";
                return false;
            }


                return true;
        }
        private long SubtrairData(DateTime data1, DateTime data2)
        {
            return (data1 - data2).Days;
        }


    }
}