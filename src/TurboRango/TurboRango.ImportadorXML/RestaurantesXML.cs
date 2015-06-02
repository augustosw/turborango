﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TurboRango.Dominio;

namespace TurboRango.ImportadorXML
{
    public class RestaurantesXML
    {
        public String NomeArquivo { get; private set; }

        IEnumerable<XElement> restaurantes;


        /// <summary>
        /// Constrói RestaurantesXML a partir de um nome de arquivo.
        /// </summary>
        /// <param name="nomeArquivo">Nome do arquivo XML a ser manipulado</param>

        public RestaurantesXML(String nomeArquivo)
        {
            NomeArquivo = nomeArquivo;
            restaurantes = XDocument.Load(NomeArquivo).Descendants("restaurante");
        }

        public IList<string> ObterNomes()
        {
            //var resultado = new List<string>();

            //var nodos = XDocument.Load(NomeArquivo).Descendants("restaurante");

            //foreach (var item in nodos)
            //{
            //    resultado.Add(item.Attribute("nome").Value);
            //}

            //return resultado;

            
            //var res = restaurantes
            //    .Select(n => new Restaurante{
            //        Nome = n.Attribute("nome").Value,
            //        Capacidade = Convert.ToInt32(n.Attribute("capacidade").Value
            //    });
            //return res.Where(x => x.Capacidade <100).Select(x => x.Nome).OrderBy(x => x);
            
            return (
                from n in restaurantes
                orderby n.Attribute("nome").Value
                where Convert.ToInt32(n.Attribute("capacidade").Value) < 100
                select n.Attribute("nome").Value
            ).ToList();

        }

        public double CapacidadeMedia()
        {
            return (
                from n in restaurantes
                select Convert.ToInt32(n.Attribute("capacidade").Value)
              ).Average();
        }

        public double CapacidadeMaxima()
        {
            var mad = (
                 from n in restaurantes
                 select Convert.ToInt32(n.Attribute("capacidade").Value)
               );
            return mad.Max();
        }

        public IList<Restaurante> AgruparPorCategoria()
        {
            var res = from n in restaurantes
                      group n by n.Attribute("categoria").Value into g
                      select new { 
                          Categoria = g.Key, 
                          Restaurantes = g.ToList(),
                          SomatorioCapacidades = g.Sum(x => Convert.ToInt32(x.Attribute("capacidade").Value))
                      };

            return null;
        }
    }
}