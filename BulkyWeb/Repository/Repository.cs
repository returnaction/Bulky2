using BulkyWeb.Data;
using BulkyWeb.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace BulkyWeb.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbset;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbset = _db.Set<T>();
        }

        public void Add(T entity)
        {
            dbset.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProoperties = null)
        {
            IQueryable<T> query = dbset;
            query = query.Where(filter);

            if (!string.IsNullOrEmpty(includeProoperties))
            {
                foreach(var includeProp in includeProoperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.FirstOrDefault();
        }

        //Category, CoverType
        public IEnumerable<T> GetAll(string? includeProoperties = null)
        {
            IQueryable<T> query = dbset;
            if(!string.IsNullOrEmpty(includeProoperties))
            {
                foreach(var includeProp in includeProoperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbset.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbset.RemoveRange(entity);
        }
    }
}
