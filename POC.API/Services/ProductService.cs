using POC.API.Interfaces.Repository;
using POC.API.Interfaces.Services;
using POC.API.Model;
using POC.API.Model.Validations;

namespace POC.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Product Create(Product newProduct)
        {
            if (!Validate(newProduct)) throw new ArgumentException("Entidade invalida");

            var dbProduct = _productRepository.Add(newProduct);

            return dbProduct;

        }

        public void Delete(int id)
        {
            if(_productRepository.GetById(id) == null) throw new ArgumentException("Entidade invalida");
            
            _productRepository.Remove(id);
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        public Product Update(Product updateProduct)
        {
            if (!Validate(updateProduct)) throw new ArgumentException("Entidade invalida");

            var dbProduct = _productRepository.Update(updateProduct);

            return dbProduct;
        }

        protected bool Validate(Product entity)
        {
            var validation = new ProductValidation();
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            return false;
        }
    }
}
