using Cortes.Repositorio.Interfaces.IUsuarioRepositorio;
using Cortes.Repositorio.Repositorios.UsuarioRepositorio;
using Microsoft.Extensions.DependencyInjection;

namespace Cortes.Repositorio.Config
{
    public static class ConfiguracaoRepositoriosExtension
    {
        public static void ConfigurarRepositorios(this IServiceCollection services)
        {
            services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
        }
    }
}
