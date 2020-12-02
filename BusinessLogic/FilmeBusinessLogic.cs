using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.DataAccess;
using WebApplication1.Models;

namespace WebApplication1.BusinessLogic
{
    public class FilmeBusinessLogic
    {
        FilmeDataAccess dataAccessFilme = new FilmeDataAccess();

        public List<FilmeEntity> Listar(out string mensagemErro)
        {
            var toReturn = dataAccessFilme.Listar(out mensagemErro);

            if (string.IsNullOrWhiteSpace(mensagemErro))
            {
                return toReturn;
            }

            return null;
        }

        public FilmeEntity Obter(long id, out string mensagemErro)
        {
            var toReturn = dataAccessFilme.Obter(id, out mensagemErro);

            if (string.IsNullOrWhiteSpace(mensagemErro))
            {
                return toReturn;
            }

            return null;
        }


        public bool ExcluirFilme(long IdFilme, out string mensagemErro)
        {
            return dataAccessFilme.ExcluirFilme(IdFilme, out mensagemErro);
        }

        public bool SalvarFilme(FilmeEntity filmeEntity, out string mensagemErro)
        {
            return dataAccessFilme.SalvarFilme(filmeEntity, out mensagemErro);
        }
    }
}