using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurboRango.Dominio
{
    public class Localizacao : Entidade
    {
        public String Bairro { get; set; }
        public String Logradouro { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
