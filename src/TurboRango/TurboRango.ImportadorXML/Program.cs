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

            //var nomes = restaurantes.OrdenarPorNomeAsc();
            //var sites = restaurantes.ObterSites();
            //var capacidademedia = restaurantes.CapacidadeMedia();
            //var capacidadeMaxima = restaurantes.CapacidadeMaxima();
            //var todos = restaurantes.TodosRestaurantes();
            #endregion

            #region ADO.NET

            //var connString = @"Data Source=.\server; Initial Catalog=TurboRango_dev;Integrated Security=True;";
            ////Se for autenticação a mão trocar Integrated Security para UID=usuario;PWD=senha

            //var acessoAoBanco = new CarinhaQueManipulaOBanco(connString);

            //acessoAoBanco.Inserir(new Contato
            //{ 
            //    Site = "www.dogao.gif",
            //    Telefone = "55555555"
            //});

            //IEnumerable<Contato> contatos = acessoAoBanco.GetContatos();

            #endregion

            #region Tema Restaurantes

            var connectionString = @"Data Source=.\server;Initial Catalog=TurboRango_dev;Integrated Security=True;";
            var restaurantesTema = new Restaurantes(connectionString);

            Restaurante dogao = new Restaurante();
            Contato contatoDogao = new Contato();
            Localizacao localizacaoDogao = new Localizacao();
            dogao.Nome = "Dogão";
            dogao.Capacidade = 100;
            dogao.Categoria = Categoria.Fastfood;
            contatoDogao.Site = "www.siteDogao.com.br";
            contatoDogao.Telefone = "35689457";
            localizacaoDogao.Bairro = "Bairro Dogao";
            localizacaoDogao.Logradouro = "Predio Dogao, numero do dogao";
            localizacaoDogao.Latitude = -25.3568;
            localizacaoDogao.Longitude = 59.55688;
            dogao.Contato = contatoDogao;
            dogao.Localizacao = localizacaoDogao;
            
            

            var rests = restaurantes.TodosRestaurantes().ToList();

            //foreach (var restaurante in rests)
            //{
            //    restaurantesTema.Inserir(restaurante);
            //}

            var todos = restaurantesTema.Todos().ToList();

            //restaurantesTema.Atualizar(18, dogao);
            //restaurantesTema.Inserir(dogao);
            restaurantesTema.Remover(18);



            #endregion

        }
    }
}
