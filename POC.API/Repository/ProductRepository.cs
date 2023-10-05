using POC.API.Interfaces.Repository;
using POC.API.Model;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace POC.API.Repository
{
    public  class ProductRepository : IProductRepository
    {
        private static List<Product> _db = new List<Product>();

        public Product Add(Product entity)
        {
            var id = _db.LastOrDefault() != null ? _db.OrderBy(x => x.Id).LastOrDefault().Id++ : 1;
            entity.Id = id;

            _db.Add(entity);

            return entity;
        }

        public List<Product> GetAll()
        {
            return _db.OrderBy(x => x.Id).ToList();
        }

        public Product GetById(int id)
        {
            return _db.OrderBy(x => x.Id).Where(x => x.Id == id).FirstOrDefault();
        }

        public void Remove(int id)
        {
            _db.Remove(_db.Where(x => x.Id == id).FirstOrDefault());
        }

        public Product Update(Product entity)
        {
            var index = _db.IndexOf(entity);
            _db[index] = entity;

            return entity;
        }

    }
}
