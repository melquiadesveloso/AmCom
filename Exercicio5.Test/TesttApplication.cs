using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Questao5.Application.Services;
using Questao5.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercicio5.Test
{
    internal class TesttApplication : WebApplicationFactory<Program>
    {
        public string Message { get; set; }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(s =>
            {
                s.AddScoped<IContaCorrenteMovimentoService, ContaCorrenteMovimentoService>();
                s.AddScoped<IContaCorrenteService, ContaCorrenteService>();
                   
            });

            return base.CreateHost(builder);
        }
    }
}
