using System.Collections.Generic;

namespace BuffetDesigner.Domain._Base
{
    public interface IRepository<TEntity>
    {
         TEntity GetById(int id);
         void Add(TEntity entity);
         
    }
}