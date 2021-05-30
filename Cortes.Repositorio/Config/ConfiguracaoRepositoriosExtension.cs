using Cortes.Repositorio.Interfaces.IAgendamentoRepositorio;
using Cortes.Repositorio.Interfaces.IEstabelecimentoRepositorio;
using Cortes.Repositorio.Interfaces.IUsuarioRepositorio;
using Cortes.Repositorio.Repositorios.AgendamentoRepositorio;
using Cortes.Repositorio.Repositorios.EstabelecimentoRepositorio;
using Cortes.Repositorio.Repositorios.UsuarioRepositorio;
using Microsoft.Extensions.DependencyInjection;

namespace Cortes.Repositorio.Config
{
    public static class ConfiguracaoRepositoriosExtension
    {
        public static void ConfigurarRepositorios(this IServiceCollection services)
        {
            services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddTransient<IAgendamentoRepositorio, AgendamentoRepositorio>();
            services.AddTransient<IEstabelecimentoRepositorio, EstabelecimentoRepositorio>();
        }
    }
}
