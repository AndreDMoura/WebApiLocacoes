using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    [Serializable()]
    public class LocacaoEntity
    {
        [Key]
        [Required]
        public long? IdLocacao { get; set; }

        [Required]
        public long? IdCliente { get; set; }

        [Required]
        public long? IdFilme { get; set; }

        [Required]
        public DateTime DtLocacao { get; set; }

        public DateTime? DtDevolucao { get; set; }

        public bool? Atraso { get; set; }

        [Required]
        public bool Ativo { get; set; }

        public string MensagemErro { get; set; }
    }
}