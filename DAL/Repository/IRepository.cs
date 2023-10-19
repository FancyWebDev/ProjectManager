﻿namespace DAL.Repository;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T Get(Func<T, bool> predicate, bool noTracking = false);
    void Create(T item);
    void Update(T item);
    void Delete(T item);
    bool Exist(Func<T, bool> predicate);
    void Save();
}