using ComicBooks.Data;
using ComicBooks.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBooks.Repository
{
    public class GenericRepository : IGenericRepository
    {
        private ApplicationDbContext _db;

        public GenericRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return _db.Set<T>().AsQueryable();
        }

        public void Add<T>(T entityToCreate) where T : class
        {
            _db.Set<T>().Add(entityToCreate);
            SaveChanges();
        }

        public void Update<T>(T entityToUpdate) where T : class
        {
            _db.Set<T>().Update(entityToUpdate);
            SaveChanges();
        }

        public void Delete<T>(T entityToDelete) where T : class
        {
            _db.Set<T>().Remove(entityToDelete);
            SaveChanges();
        }

        public IQueryable<T> SqlQuery<T>(string sql, params object[] parameters) where T : class
        {
            return _db.Set<T>().FromSql(sql, parameters);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
