﻿namespace DbTextEditor.Model.DAL.Interfaces
{
    public interface IRepository<TEntity>
    {
        bool Exists(string key);
        void Create(TEntity entity);
        TEntity Get(string key);
        void Update(TEntity entity);
        void Delete(string key);
    }
}