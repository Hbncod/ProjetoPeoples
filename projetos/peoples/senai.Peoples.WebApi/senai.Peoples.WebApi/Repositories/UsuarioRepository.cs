using senai.Peoples.WebApi.Domains;
using senai.Peoples.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.Peoples.WebApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        //private string stringConexao = "Data Source=DEV102\\SQLEXPRESS; initial catalog=Peoples; integrated security=true;";
        private string stringConexao = "Data Source=DEV102\\SQLEXPRESS; initial catalog=Peoples; user Id=sa; pwd=sa@132";

        public UsuarioDomain BuscarPorEmail(string email)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryBuscarEmail = "SELECT Email FROM Usuarios WHERE Email = @EMAIL";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryBuscarEmail,con))
                {
                    cmd.Parameters.AddWithValue("@EMAIL", email);

                    var rdr = cmd.ExecuteReader();

                    UsuarioDomain usuario = new UsuarioDomain()
                    {
                        Email = rdr[0].ToString()
                    };
                    return usuario;
                }
            }
        }

        public UsuarioDomain BuscarPorEmailSenha(string email, string senha)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Define a query a ser executada no banco

                string querySelect = "SELECT Id_Usuario, Email, Senha, Fk_TipoUsuario FROM Usuarios WHERE Email = @Email AND Senha = @Senha";

                // Define o comando passando a query e a conexão
                using (SqlCommand cmd = new SqlCommand(querySelect, con))
                {
                    // Define o valor dos parâmetros
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Senha", senha);

                    // Abre a conexão com o banco
                    con.Open();

                    // Executa o comando e armazena os dados no objeto rdr
                    SqlDataReader rdr = cmd.ExecuteReader();

                    // Caso dados tenham sido obtidos
                    if (rdr.HasRows)
                    {
                        // Cria um objeto usuario
                        UsuarioDomain usuario = new UsuarioDomain();

                        // Enquanto estiver percorrendo as linhas
                        while (rdr.Read())
                        {
                            // Atribui à propriedade IdUsuario o valor da coluna IdUsuario
                            usuario.Id_Usuario = Convert.ToInt32(rdr["Id_Usuario"]);

                            // Atribui à propriedade Email o valor da coluna Email
                            usuario.Email = rdr["Email"].ToString();

                            // Atribui à propriedade Senha o valor da coluna Senha
                            usuario.Senha = rdr["Senha"].ToString();

                            // Atribui à propriedade Permissao o valor da coluna Permissao
                            usuario.TipoUsuario = Convert.ToInt32(rdr["Fk_TipoUsuario"]);
                        }

                        // Retorna o objeto usuario
                        return usuario;
                    }
                    return null;
                }
            }
        }

        public void Cadastrar(UsuarioDomain novoUsuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryCadastro = "INSERT INTO Usuarios(Email,Senha,Fk_TipoUsuario) VALUES (@EMAIL,@SENHA,@TIPO)";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryCadastro,con))
                {
                    cmd.Parameters.AddWithValue("@EMAIL", novoUsuario.Email);
                    cmd.Parameters.AddWithValue("SENHA", novoUsuario.Senha);
                    cmd.Parameters.AddWithValue("TIPO", novoUsuario.TipoUsuario);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<UsuarioDomain> ListarTodos()
        {
            List<UsuarioDomain> Usuarios = new List<UsuarioDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryListartodos = "SELECT Id_Usuario, Email, Senha, Fk_TipoUsuario, TipoUsuario.Tipo FROM Usuarios INNER JOIN TipoUsuario ON Id_TipoUsuario = Fk_TipoUsuario";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryListartodos,con))
                {
                    SqlDataReader rdr;
                    rdr = cmd.ExecuteReader();
                    while(rdr.Read())
                    {
                        UsuarioDomain usuario = new UsuarioDomain()
                        {
                            Id_Usuario = Convert.ToInt32(rdr[0]),
                            Email = rdr[1].ToString(),
                            Senha = rdr[2].ToString(),
                            TipoUsuario = Convert.ToInt32(rdr[3]),
                            Fk_TipoUsuario = new TipoUsuarioDomain()
                            {
                                Id_TipoUsuario = Convert.ToInt32(rdr[3]),
                                Tipo = rdr[4].ToString()
                            }

                        };
                        Usuarios.Add(usuario);
                    }
                }
                return Usuarios;
            }
        }
    }
}
