using Cortes.Services.Interfaces;
using Cortes.Services.Interfaces.AgendamentoServico;
using Cortes.Services.Servicos;
using Cortes.Services.UsuarioServicos;
using Microsoft.Extensions.DependencyInjection;

namespace Cortes.Services.Config
{
    public static class ConfiguracaoServicosExtension
    {
        public static void ConfigurarServicos(this IServiceCollection services)
        {
            services.AddTransient<IUsuarioServico, UsuarioServico>();
            services.AddTransient<IAgendamentoServico, AgendamentoServico>();
        }
    }
}
