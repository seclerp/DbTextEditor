namespace DbTextEditor.Model.Storage
{
    public interface IRepository<TEntity, TKey>
    {
        void Create(TEntity entity);
        TEntity Get(TKey key);
        void Update(TEntity entity);
        void Delete(TKey key);
    }
}