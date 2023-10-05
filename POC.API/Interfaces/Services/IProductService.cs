using POC.API.Model;

namespace POC.API.Interfaces.Services
{
    public interface IProductService
    {
        Product Create(Product newProduct);
        List<Product> GetAll();
        Product GetById(int id);
        Product Update(Product updateProduct);
        void Delete(int id);
    }
}
