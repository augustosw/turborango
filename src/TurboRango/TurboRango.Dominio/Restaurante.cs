using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TurboRango.Dominio
{
    public class Restaurante : Entidade
    {
        public String Nome { get; set; }
        public int Capacidade { get; set; }
        public virtual Localizacao Localizacao { get; set; }
        public virtual Contato Contato { get; set; }
        public Categoria Categoria { get; set; }
    }
}
