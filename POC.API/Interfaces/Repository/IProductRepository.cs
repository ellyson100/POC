using POC.API.Model;
using System.Linq.Expressions;

namespace POC.API.Interfaces.Repository
{
    public interface IProductRepository
    {
        Product Add(Product entity);
        Product GetById(int id);
        List<Product> GetAll();
        Product Update(Product entity);
        void Remove(int id);
    }
}
