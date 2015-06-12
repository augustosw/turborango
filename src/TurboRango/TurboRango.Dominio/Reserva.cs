using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboRango.Dominio
{
    public class Reserva : Entidade
    {
        public Restaurante Restaurante { get; set; }
        public DateTime Data { get; set; }
        public int QtdePessoas { get; set; }
        public String Nome { get; set; }
        public String Telefone { get; set; }
        public decimal ValorTotal { get; set; }

        public Reserva()
        {
        }
    }
}
