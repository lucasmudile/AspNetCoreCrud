using FN.Store.Domain.Contracts.Repositories;
using FN.Store.Domain.Entities;

namespace FN.Store.Data.EF.Repositories
{
    public class CategoriaRepositoryEF : RepositoryEF<Categoria>,ICateogriaRepository
    {
        public CategoriaRepositoryEF(StoreDataContext ctx) : base(ctx)
        {
        }
    }
}
