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
        public Reserva Reserva { get; set; }
    }
}