using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace senai.Peoples.WebApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                // Adiciona o MVC ao projeto
                .AddMvc()

                // Define a versão do .NET Core
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);

            services
                    //define a forma de autenticação
                    .AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = "JwtBearer";
                        options.DefaultChallengeScheme = "JwtBearer";
                    })
                    //Define os parâmetros de validação do token
                    .AddJwtBearer("JwtBearer", options =>
                    {
                        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                        {
                                //qm está solicitando
                                ValidateIssuer = true,

                                //qm está validando
                                ValidateAudience = true,

                                //Definindo q o tempo de expiração está ativo
                                ValidateLifetime = true,

                                //definindo a forma de criptografia
                                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("usuarios-chave-autenticacao")),

                                //Tempo de expiração do token
                                ClockSkew = TimeSpan.FromMinutes(30),

                                //Nome da isseur, de onde está vindo
                                ValidIssuer = "Peoples.WebApi",

                                //Nome da Isseus, de onde está indo
                                ValidAudience = "Peoples.WebApi"
                        };
                    }
                    );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Define o uso do MVC
            app.UseMvc();
        }
    }
}
