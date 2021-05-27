using Cortes.Services.Interfaces;
using Cortes.Services.UsuarioServicos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cortes.Services.Config
{
    public static class ConfiguracaoServicosExtension
    {
        public static void ConfigurarServicos(this IServiceCollection services)
        {
            services.AddTransient<IUsuarioServico, UsuarioServico>();
        }
    }
}
