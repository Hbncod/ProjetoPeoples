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
    public class UsuarioController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }


        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        
        [HttpPost]
        public IActionResult Cadastrar(UsuarioDomain novoUsuario)
        {
            UsuarioDomain usuarioExiste = _usuarioRepository.BuscarPorEmail(novoUsuario.Email);
            if (usuarioExiste != null)
            {
                return NotFound("Este Email já está cadastrado");
            }
            _usuarioRepository.Cadastrar(novoUsuario);
            // Retorna o status code 201 - Created com a URI e o objeto cadastrado
            return Created("http://localhost:5000/api/Usuario", novoUsuario);
        }


    }
}