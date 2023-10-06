using POC.API.Repository;
using POC.API.Services;

namespace POC.UnitTest
{
    public class ProductTest
    {

        [Fact(DisplayName = "Test Create Success")]
        [Trait("ProductServiceTest", "ProductServiceTest Unit Tests")]
        public void TestCreateWithSucess()
        {
            // Arrange
            var repository = new ProductRepository();
            var service = new ProductService(repository);

            // Act
            var product = new GenerateDataTest().GetValid();
            var productDb = service.Create(product);

            // Assert
            Assert.NotNull(product);
            Assert.NotNull(productDb);
            Assert.True(productDb.Id > 0);
        }

        [Fact(DisplayName = "Test Create Failure")]
        [Trait("ProductServiceTest", "ProductServiceTest Unit Tests")]
        public void TestCreateWithFailure()
        {
            // Arrange
            var repository = new ProductRepository();
            var service = new ProductService(repository);

            // Act
            var product = new GenerateDataTest().GetInvalid();

            // Assert
            Assert.Throws<ArgumentException>(() => service.Create(product));
        }

        [Fact(DisplayName = "Test Get Success")]
        [Trait("ProductServiceTest", "ProductServiceTest Unit Tests")]
        public void TestGetWithSucess()
        {
            // Arrange
            var repository = new ProductRepository();
            var service = new ProductService(repository);

            // Act
            var product = new GenerateDataTest().GetValid();
            var productDb = service.Create(product);
            var productGet = service.GetById(productDb.Id);

            // Assert
            Assert.NotNull(product);
            Assert.NotNull(productDb);
            Assert.NotNull(productGet);
            Assert.True(productDb.Id > 0);
            Assert.True(productGet.Id > 0);
        }

        [Fact(DisplayName = "Test Get Failure")]
        [Trait("ProductServiceTest", "ProductServiceTest Unit Tests")]
        public void TestGetWithFailure()
        {
            // Arrange
            var repository = new ProductRepository();
            var service = new ProductService(repository);

            // Act
            var productGet = service.GetById(-1);

            // Assert
            Assert.Null(productGet);
        }

        [Fact(DisplayName = "Test GetAll Success")]
        [Trait("ProductServiceTest", "ProductServiceTest Unit Tests")]
        public void TestGetAllWithSucess()
        {
            // Arrange
            var repository = new ProductRepository();
            var service = new ProductService(repository);

            // Act
            for (var i = 0; i < 10; i++)
            {
                var product = new GenerateDataTest().GetValid();
                var productDb = service.Create(product);
            }
            
            var all = service.GetAll();

            // Assert
            Assert.NotNull(all);
            Assert.NotEmpty(all);
        }

        [Fact(DisplayName = "Test GetAll Failure")]
        [Trait("ProductServiceTest", "ProductServiceTest Unit Tests")]
        public void TestGetAllWithFailure()
        {
            // Arrange
            var repository = new ProductRepository();
            var service = new ProductService(repository);

            // Act
            var all = service.GetAll();

            // Assert
            Assert.Empty(all);
        }

        [Fact(DisplayName = "Test Update Success")]
        [Trait("ProductServiceTest", "ProductServiceTest Unit Tests")]
        public void TestUpdateWithSucess()
        {
            // Arrange
            var repository = new ProductRepository();
            var service = new ProductService(repository);

            // Act
            var product = new GenerateDataTest().GetValid();
            var productDb = service.Create(product);
            var productName = product.Name;
            product = null;
            productDb.Name = "Edit";
            var productDbUptade = service.Update(productDb);

            // Assert
            Assert.Null(product);
            Assert.NotNull(productDb);
            Assert.NotNull(productDbUptade);
            Assert.True(productDb.Id > 0);
            Assert.NotEqual(productName, productDbUptade.Name);
        }

        [Fact(DisplayName = "Test Update Failure")]
        [Trait("ProductServiceTest", "ProductServiceTest Unit Tests")]
        public void TestUpdateWithFailure()
        {
            // Arrange
            var repository = new ProductRepository();
            var service = new ProductService(repository);

            // Act
            var product = new GenerateDataTest().GetValid();
            var productDb = service.Create(product);

            var productInvalid = new GenerateDataTest().GetInvalid();

            // Assert
            Assert.NotNull(product);
            Assert.NotNull(productDb);
            Assert.True(productDb.Id > 0);
            Assert.Throws<ArgumentException>(() => service.Update(productInvalid));
        }

        [Fact(DisplayName = "Test Delete Success")]
        [Trait("ProductServiceTest", "ProductServiceTest Unit Tests")]
        public void TestDeleteWithSucess()
        {
            // Arrange
            var repository = new ProductRepository();
            var service = new ProductService(repository);

            // Act
            var product = new GenerateDataTest().GetValid();
            var productDb = service.Create(product);
            service.Delete(product.Id);
            var delete = service.GetById(product.Id);

            // Assert
            Assert.NotNull(product);
            Assert.NotNull(productDb);
            Assert.True(productDb.Id > 0);
            Assert.Null(delete);
        }

        [Fact(DisplayName = "Test Delete Failure")]
        [Trait("ProductServiceTest", "ProductServiceTest Unit Tests")]
        public void TestDeleteWithFailure()
        {
            var repository = new ProductRepository();
            var service = new ProductService(repository);

            Assert.Throws<ArgumentException>(() => service.Delete(-1));
        }
    }
}
