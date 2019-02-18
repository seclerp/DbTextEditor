using System.Collections.Generic;
using DbTextEditor.Model.Entities;

namespace DbTextEditor.Model.DAL.Interfaces
{
    public interface IDbFilesRepository : IRepository<DbFileEntity>
    {
        IEnumerable<DbFileEntity> GetAll();
    }
}