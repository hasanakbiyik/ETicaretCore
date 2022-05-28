using AppCore.Records.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.DataAccess.Bases
{
    public interface IRepositoryBase<TEntity, TDbContext>: IDisposable where TEntity : RecordBase, new() where TDbContext : DbContext, new()
    {
        TDbContext DbContext { get; set; }
        IQueryable<TEntity> Query(); 

        void Add(TEntity entity,bool save =true);
        void Update(TEntity entity, bool save = true);
        void Delete(TEntity entity, bool softDelete = false, bool save = true);
        int Save();

    }
}
