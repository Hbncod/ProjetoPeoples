using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using senai.Peoples.WebApi.Domains;
using senai.Peoples.WebApi.Interfaces;
using senai.Peoples.WebApi.Repositories;

namespace senai.Peoples.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }


        public LoginController()
        {
            _usuarioRepository = new UsuarioRepository();
        }
        [HttpPost]
        public IActionResult Post(UsuarioDomain login)
        {

            UsuarioDomain usuarioBuscado = _usuarioRepository.BuscarPorEmailSenha(login.Email, login.Senha);
            if (usuarioBuscado == null)
            {
                return NotFound("Email ou Senha Inválidos");
            }

            //se houver usuario continuar com a criação do token

            //define os dados q serão fornecidos no token --Payload

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                new Claim(JwtRegisteredClaimNames.Jti,   usuarioBuscado.Id_Usuario.ToString()),
                new Claim(ClaimTypes.Role, usuarioBuscado.TipoUsuario.ToString())
            };

            //Define a Chave de acesso do token
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("usuarios - chave - autenticacao"));

            //Define as credenciais do tonken - Header
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Gera o token
            var token = new JwtSecurityToken(
                issuer: "Peoples.WebApi",  //Emissor do token
                audience: "Peoples.WebApi",//destinatario do token
                claims: claims,             //dados definidos acima
                expires: DateTime.Now.AddMinutes(30),   //tempo de expiracao
                signingCredentials: creds               //credenciais do token
                );
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}