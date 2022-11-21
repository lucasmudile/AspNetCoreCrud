using FN.Store.Data.EF;
using FN.Store.Data.EF.Repositories;
using FN.Store.Domain.Contracts.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FN.Store.DI
{
    public static class ConfigService
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            //Instancia única em todo projecto
            // services.AddSingleton < StoreDataContext >();
            
            //Por chamada
            // services.AddTransient< StoreDataContext >();

            //Por requisição
            services.AddScoped <StoreDataContext>();
            services.AddTransient<IProdutoRepository,ProdutoRepositoryEF>();
            services.AddTransient<ICateogriaRepository, CategoriaRepositoryEF>();
        }
    }
}
