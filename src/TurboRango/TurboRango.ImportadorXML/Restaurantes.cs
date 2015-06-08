using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboRango.Dominio;

namespace TurboRango.ImportadorXML
{
    class Restaurantes
    {
        internal string ConnectionString { get; private set; }

        public Restaurantes(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }

        public void Inserir(Restaurante restaurante)
        {
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                string comandoSQL = "INSERT INTO [dbo].[Restaurante] ([Nome], [Capacidade], [Categoria], [ContatoId], [LocalizacaoId]) VALUES (@Nome, @Capacidade, @Categoria, @ContatoId, @LocalizacaoId)";
                using (var inserirRestaurante = new SqlCommand(comandoSQL, connection))
                {
                    inserirRestaurante.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = restaurante.Nome;
                    inserirRestaurante.Parameters.Add("@Capacidade", SqlDbType.Int).Value = restaurante.Capacidade;
                    inserirRestaurante.Parameters.Add("@Categoria", SqlDbType.NVarChar).Value = restaurante.Categoria.ToString();
                    inserirRestaurante.Parameters.Add("@ContatoId", SqlDbType.Int).Value = InserirContato(restaurante.Contato);
                    inserirRestaurante.Parameters.Add("@LocalizacaoId", SqlDbType.Int).Value = InserirLocalizacao(restaurante.Localizacao);

                    connection.Open();
                    inserirRestaurante.ExecuteNonQuery();
                }
            }
        }

        private int InserirContato(Contato contato)
        {
            int idCriado = 0;
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                string comandoSQL = "INSERT INTO [dbo].[Contato] ([Site], [Telefone]) VALUES (@Site, @Telefone)";
                string id = "SELECT @@IDENTITY";
                using (var inserirContato = new SqlCommand(comandoSQL, connection))
                {
                    inserirContato.Parameters.Add("@Site", SqlDbType.NVarChar).Value = contato.Site ?? (object)DBNull.Value;
                    inserirContato.Parameters.Add("@Telefone", SqlDbType.NVarChar).Value = contato.Telefone ?? (object)DBNull.Value;

                    connection.Open();
                    inserirContato.ExecuteNonQuery();
                    inserirContato.CommandText = id;
                    idCriado = Convert.ToInt32(inserirContato.ExecuteScalar());
                }
            }
            return idCriado;
        }
        private int InserirLocalizacao(Localizacao localizacao)
        {
            int idCriado = 0;
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                string comandoSQL = "INSERT INTO [dbo].[Localizacao] ([Bairro], [Logradouro], [Latitude], [Longitude]) VALUES (@Bairro, @Logradouro, @Latitude, @Longitude)";
                string id = "SELECT @@IDENTITY";
                using (var inserirLocalizacao = new SqlCommand(comandoSQL, connection))
                {
                    inserirLocalizacao.Parameters.Add("@Bairro", SqlDbType.NVarChar).Value = localizacao.Bairro;
                    inserirLocalizacao.Parameters.Add("@Logradouro", SqlDbType.NVarChar).Value = localizacao.Logradouro;
                    inserirLocalizacao.Parameters.Add("@Latitude", SqlDbType.Float).Value = localizacao.Latitude;
                    inserirLocalizacao.Parameters.Add("@Longitude", SqlDbType.Float).Value = localizacao.Longitude;

                    connection.Open();
                    inserirLocalizacao.ExecuteNonQuery();
                    inserirLocalizacao.CommandText = id;
                    idCriado = Convert.ToInt32(inserirLocalizacao.ExecuteScalar());
                }
            }

            return idCriado;
        }

        public void Remover(int id)
        {
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                string comandoSQL = "DELETE FROM [dbo].[Restaurante] WHERE Id = @Id";
                using (var removeRestaurante = new SqlCommand(comandoSQL, connection))
                {
                    removeRestaurante.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    connection.Open();
                    removeRestaurante.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Restaurante> Todos()
        {
            List<Restaurante> todos = new List<Restaurante>();
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                string comandoSQL = "SELECT R.Nome, R.Capacidade, R.Categoria, C.Site, C.Telefone, L.Bairro, L.Logradouro, L.Latitude, L.Longitude"
                                 + " FROM Restaurante R, Contato C, Localizacao L"
                                 + " WHERE R.ContatoId = C.Id AND R.LocalizacaoId = L.Id";
                using (var todosRestaurantes = new SqlCommand(comandoSQL, connection))
                {
                    connection.Open();
                    var resultado = todosRestaurantes.ExecuteReader();

                    while (resultado.Read())
                    {
                        todos.Add(new Restaurante{
                            Nome = resultado.GetString(0),
                            Capacidade = resultado.GetInt32(1),
                            Categoria = (Categoria)Enum.Parse(typeof(Categoria), resultado.GetString(2), ignoreCase: true),
                            Contato = new Contato{
                                Site = resultado.IsDBNull(3) ? null : resultado.GetString(3),
                                Telefone = resultado.IsDBNull(4) ? null : resultado.GetString(4),
                            },
                            Localizacao = new Localizacao{
                                Bairro = resultado.GetString(5),
                                Logradouro = resultado.GetString(6),
                                Latitude = resultado.GetDouble(7),
                                Longitude = resultado.GetDouble(8),
                            },
                        });
                    }
                }
            }
            
            return todos;
        }

        public void Atualizar(int id, Restaurante restaurante)
        {
            Contato contato = restaurante.Contato;
            Localizacao localizacao = restaurante.Localizacao;

            AtualizarContato(id, contato);
            AtualizarLocalizacao(id, localizacao);

            using (var connection = new SqlConnection(this.ConnectionString))
            {
                string comandoSQL = "UPDATE Restaurante"
                                  + " SET Nome = @Nome, Capacidade = @Capacidade, Categoria = @Categoria"
                                  + " WHERE Id = @Id";
                using (var atualizaRestaurante = new SqlCommand(comandoSQL, connection))
                {
                    atualizaRestaurante.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = restaurante.Nome;
                    atualizaRestaurante.Parameters.Add("@Capacidade", SqlDbType.Int).Value = restaurante.Capacidade;
                    atualizaRestaurante.Parameters.Add("@Categoria", SqlDbType.NVarChar).Value = restaurante.Categoria.ToString();
                    atualizaRestaurante.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    connection.Open();
                    atualizaRestaurante.ExecuteNonQuery();
                }
            }
        }

        private void AtualizarContato(int id, Contato contato)
        {
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                string comandoSQL = "UPDATE Contato"
	                              +" SET Site = @Site, Telefone = @Telefone"
                                  +" FROM Contato C, Restaurante R"
                                  +" WHERE R.Id = @Id AND R.ContatoId = C.Id";
                using (var atualizaContato = new SqlCommand(comandoSQL, connection))
                {
                    atualizaContato.Parameters.Add("@Site", SqlDbType.NVarChar).Value = contato.Site ?? (object)DBNull.Value;
                    atualizaContato.Parameters.Add("@Telefone", SqlDbType.NVarChar).Value = contato.Telefone ?? (object)DBNull.Value;
                    atualizaContato.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    connection.Open();
                    atualizaContato.ExecuteNonQuery();
                }
            }
        }
        private void AtualizarLocalizacao(int id, Localizacao localizacao)
        {
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                string comandoSQL = "UPDATE localizacao"
                                 + " SET Bairro = @Bairro, Logradouro = @Logradouro, Latitude = @Latitude, Longitude = @Longitude"
                                 + " FROM Localizacao L, Restaurante R"
                                 + " WHERE R.Id = @Id AND R.LocalizacaoId = L.Id";
                using (var atualizaLocalizacao = new SqlCommand(comandoSQL, connection))
                {
                    atualizaLocalizacao.Parameters.Add("@Bairro", SqlDbType.NVarChar).Value = localizacao.Bairro;
                    atualizaLocalizacao.Parameters.Add("@Logradouro", SqlDbType.NVarChar).Value = localizacao.Logradouro;
                    atualizaLocalizacao.Parameters.Add("@Latitude", SqlDbType.Float).Value = localizacao.Latitude;
                    atualizaLocalizacao.Parameters.Add("@Longitude", SqlDbType.Float).Value = localizacao.Longitude;
                    atualizaLocalizacao.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    connection.Open();
                    atualizaLocalizacao.ExecuteNonQuery();
                }
            }
        }
    }
}
