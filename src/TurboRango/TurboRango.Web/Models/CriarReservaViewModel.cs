using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TurboRango.Dominio;

namespace TurboRango.Web.Models
{
    public class CriarReservaViewModel
    {
        public int RestauranteId { get; set; }
        public DateTime Data { get; set; }
        public int QtdePessoas { get; set; }
        public String Nome { get; set; }
        public String Telefone { get; set; }
        public decimal ValorTotal { get; set; }
    }
}