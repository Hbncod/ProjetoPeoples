using senai.Peoples.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.Peoples.WebApi.Interfaces
{
    interface IUsuarioRepository
    {
        /// <summary>
        /// Buscar usuario
        /// </summary>
        /// <param name="email"></param>
        /// <param name="senha"></param>
        /// <returns>um usuario</returns>
        UsuarioDomain BuscarPorEmailSenha(string email, string senha);

        void Cadastrar(UsuarioDomain novoUsuario);

        UsuarioDomain BuscarPorEmail(string email);

        List<UsuarioDomain> ListarTodos();

    }
}
