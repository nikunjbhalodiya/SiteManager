using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(object id);
        void Add(T item);
        void Delete(T item);
        void Update(T item);
        void Save();
    }
}
