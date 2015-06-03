using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboRango.ImportadorXML
{
    class Program
    {
        static void Main(string[] args)
        {
            const string nomeArquivo = "restaurantes.xml";

            RestaurantesXML restaurantes = new RestaurantesXML(nomeArquivo);

            var nomes = restaurantes.OrdenarPorNomeAsc();
            var sites = restaurantes.ObterSites();
            var capacidademedia = restaurantes.CapacidadeMedia();
            var capacidadeMaxima = restaurantes.CapacidadeMaxima();

        }
    }
}
