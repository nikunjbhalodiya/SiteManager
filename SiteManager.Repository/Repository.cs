using SiteManager.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public DbSet<T> Items { get; set; }
        private SqliteContext _context;
        public Repository(SqliteContext context)
        {
            Items =  context.Set<T>();
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return Items.ToList();
        }

        public T Get(object id)
        {
            return Items.Find(id);
        }

        public void Add(T item)
        {
            Items.Add(item);
        }

        public void Delete(T item)
        {
            Items.Remove(item);
        }

        public void Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return Items.Where(predicate);
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        
    }
}

