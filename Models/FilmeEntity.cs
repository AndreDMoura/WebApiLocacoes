using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    [Serializable()]
    public class FilmeEntity
    {
        [Key]
        [Required]
        public long? IdFilme { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public int Diarias { get; set; }

        [Required]
        public bool Disponivel { get; set; }

        [Required]
        public bool Ativo { get; set; }

        public string MensagemErro { get; set; }

        public bool EstaDisponivel()
        {
            return this.Disponivel;
        }


    }
}