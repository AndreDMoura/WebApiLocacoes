using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using WebApplication1.Validation;

namespace WebApplication1.Models
{
    [Serializable()]
    public class ClienteEntity
    {
        [Key]
        [Required]
        public long? IdCliente { get; set; }

        [Required]
        [Unique(typeof(ClienteEntity), typeof(Group), "Nome", ErrorMessage = "Nome já existe. Por favor, escolha outro nome ou utilize o existente!")]

        public string Nome { get; set ; }

        [Required]
        public bool Ativo { get; set; }

        public string MensagemErro { get; set; }
    }
}