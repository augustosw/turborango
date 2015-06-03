using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboRango.Dominio;

namespace TurboRango.ImportadorXML
{
    class Program
    {
        static void Main(string[] args)
        {
            #region LINQ&XML
            const string nomeArquivo = "restaurantes.xml";

            RestaurantesXML restaurantes = new RestaurantesXML(nomeArquivo);

            var nomes = restaurantes.OrdenarPorNomeAsc();
            var sites = restaurantes.ObterSites();
            var capacidademedia = restaurantes.CapacidadeMedia();
            var capacidadeMaxima = restaurantes.CapacidadeMaxima();
            var todos = restaurantes.TodosRestaurantes();
            #endregion

            #region ADO.NET

            var connString = @"Data Source=.\server; Initial Catalog=TurboRango_dev;Integrated Security=True;";
            //Se for autenticação a mão trocar Integrated Security para UID=usuario;PWD=senha

            var acessoAoBanco = new CarinhaQueManipulaOBanco(connString);

            acessoAoBanco.Inserir(new Contato
            { 
                Site = "www.dogao.gif",
                Telefone = "55555555"
            });

            IEnumerable<Contato> contatos = acessoAoBanco.GetContatos();

            #endregion

        }
    }
}
