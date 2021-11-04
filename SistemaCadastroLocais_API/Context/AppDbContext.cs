using SistemaCadastroLocais_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCadastroLocais_API.Context
{

    public class AppDbContext : DbContext
    {
        //Construtor da classe AppDbContext
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        //É o objeto que recebe a estrutura da tabela vindo de Models
        public DbSet<TB_PontosTuristicos> locais { get; set; }
        
        //Adicionando AppSettings.Json e fazendo a conexão com o banco de dados através da ConnectionString
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //Instanciado a clase para acesso ao arquivo json
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            //buscar string de conexao
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ServerConnection"));
       
        }

    }
}
