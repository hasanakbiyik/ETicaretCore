using AppCore.DataAccess.Bases.EntityFramework;
using AppCore.Records.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Business.Services.Bases
{
    public interface IService<TModel, TEntity, TDbContext> : IDisposable where TModel : RecordBase,
        new() where TEntity : RecordBase, new() where TDbContext : DbContext, new()
    {
        RepositoryBase<TEntity, TDbContext> Repository { get; set; }
        IQueryable<TModel> Query();
        bool Add(TModel model);
        bool Update(TModel model);
        bool Delete(int id);  

    }
}
