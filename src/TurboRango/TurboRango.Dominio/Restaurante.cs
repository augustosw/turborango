using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboRango.Dominio
{
    internal class Restaurante
    {
        internal String Nome { get; set; }
        internal int Capacidade { get; set; }
        internal Localizacao Localizacao { get; set; }
        internal Contato Contato { get; set; }
        internal Categoria Catergoria { get; set; }
    }
}
