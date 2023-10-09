using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using POC.API.Model;
using POC.UnitTest;
using System.Net;

namespace POC.IntegrationTest
{
    public class ProductTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        private readonly string hostApi = "https://localhost:7107/api/Products";

        public ProductTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact(DisplayName = "Register Product with Success")]
        [Trait("ProductControllerTest", "Product Controller Tests")]
        public async Task RegisterWithSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var viewModel = new GenerateDataTest().GetViewModelValid();
            var response = await client.PostAsync(hostApi,
                new StringContent(JsonConvert.SerializeObject(viewModel), System.Text.Encoding.UTF8, "application/json"));
            string json = await response.Content.ReadAsStringAsync();
            dynamic data = JObject.Parse(json.ToString());
            Product product = JsonConvert.DeserializeObject<Product>(data.ToString());

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(product);
            Assert.Equal(viewModel.Name, product.Name);
            Assert.Equal(viewModel.Description, product.Description);
            Assert.Equal(((long)viewModel.Price), ((long)product.Price));
        }

        [Fact(DisplayName = "Register Product with Failure")]
        [Trait("ProductControllerTest", "Product Controller Tests")]
        public async Task RegisterWithFailure()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var viewModel = new GenerateDataTest().GetViewModelInvalid();
            var response = await client.PostAsync(hostApi,
                new StringContent(JsonConvert.SerializeObject(viewModel), System.Text.Encoding.UTF8, "application/json"));
            string json = await response.Content.ReadAsStringAsync();
            dynamic data = JObject.Parse(json.ToString());
            Product product = JsonConvert.DeserializeObject<Product>(data.ToString());

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Get Product with Success")]
        [Trait("ProductControllerTest", "Product Controller Tests")]
        public async Task GetWithSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var viewModel = new GenerateDataTest().GetViewModelValid();
            var response = await client.PostAsync(hostApi,
                new StringContent(JsonConvert.SerializeObject(viewModel), System.Text.Encoding.UTF8, "application/json"));
            string json = await response.Content.ReadAsStringAsync();
            dynamic data = JObject.Parse(json.ToString());
            var product = JsonConvert.DeserializeObject<Product>(data.ToString());

            var responseGet = await client.GetAsync(hostApi+"/1");
            string jsonGet = await responseGet.Content.ReadAsStringAsync();
            dynamic dataGet = JObject.Parse(jsonGet.ToString());
            var productGet = JsonConvert.DeserializeObject<Product>(dataGet.ToString());

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(product);
            Assert.Equal(HttpStatusCode.OK, responseGet.StatusCode);
            Assert.NotNull(productGet);
        }

        [Fact(DisplayName = "Get Product with Failure")]
        [Trait("ProductControllerTest", "Product Controller Tests")]
        public async Task GetWithFailure()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var responseGet = await client.GetAsync(hostApi + "/-1");
            string jsonGet = await responseGet.Content.ReadAsStringAsync();
            dynamic dataGet = JObject.Parse(jsonGet.ToString());
            var productGet = JsonConvert.DeserializeObject<Product>(dataGet.ToString());

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, responseGet.StatusCode);
        }


        [Fact(DisplayName = "GetAll Product with Success")]
        [Trait("ProductControllerTest", "Product Controller Tests")]
        public async Task GetAllWithSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            for(int i = 0; i < 10; i++)
            {
                var viewModel = new GenerateDataTest().GetViewModelValid();
                var response = await client.PostAsync(hostApi,
                    new StringContent(JsonConvert.SerializeObject(viewModel), System.Text.Encoding.UTF8, "application/json"));
                string json = await response.Content.ReadAsStringAsync();
                dynamic data = JObject.Parse(json.ToString());
            }
            

            var responseGet = await client.GetAsync(hostApi);
            string jsonGet = await responseGet.Content.ReadAsStringAsync();
            dynamic dataGet = JArray.Parse(jsonGet);
            var productGet = JsonConvert.DeserializeObject<List<Product>>(dataGet.ToString());

            // Assert
            Assert.Equal(HttpStatusCode.OK, responseGet.StatusCode);
            Assert.NotNull(productGet);
            Assert.NotEmpty(productGet);
        }


        [Fact(DisplayName = "GetAll Product with Failure")]
        [Trait("ProductControllerTest", "Product Controller Tests")]
        public async Task GetAllWithFailure()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var responseGet = await client.GetAsync(hostApi);
            string jsonGet = await responseGet.Content.ReadAsStringAsync();
            dynamic dataGet = JArray.Parse(jsonGet);
            var productGet = JsonConvert.DeserializeObject<List<Product>>(dataGet.ToString());

            // Assert
            Assert.Equal(HttpStatusCode.OK, responseGet.StatusCode);
        }

        [Fact(DisplayName = "Update Product with Success")]
        [Trait("ProductControllerTest", "Product Controller Tests")]
        public async Task UpdateWithSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var viewModel = new GenerateDataTest().GetViewModelValid();
            var response = await client.PostAsync(hostApi,
                new StringContent(JsonConvert.SerializeObject(viewModel), System.Text.Encoding.UTF8, "application/json"));
            string json = await response.Content.ReadAsStringAsync();
            dynamic data = JObject.Parse(json.ToString());
            var product = JsonConvert.DeserializeObject<Product>(data.ToString());

            var responseGet = await client.GetAsync(hostApi + "/1");
            string jsonGet = await responseGet.Content.ReadAsStringAsync();
            dynamic dataGet = JObject.Parse(jsonGet.ToString());
            var productGet = JsonConvert.DeserializeObject<Product>(dataGet.ToString());

            var productUpdate = new Product(1, viewModel.Name + " Edit", viewModel.Description + " Edit", 1);
            var responsePut = await client.PutAsync(hostApi + "/1",
                new StringContent(JsonConvert.SerializeObject(productUpdate), System.Text.Encoding.UTF8, "application/json"));
            string jsonPut = await responsePut.Content.ReadAsStringAsync();
            dynamic dataPut = JObject.Parse(jsonPut.ToString());
            var productPut = JsonConvert.DeserializeObject<Product>(dataPut.ToString());

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(product);
            Assert.Equal(HttpStatusCode.OK, responseGet.StatusCode);
            Assert.NotNull(productGet);
            Assert.Equal(HttpStatusCode.OK, responsePut.StatusCode);
            Assert.NotNull(productPut);
        }

        [Fact(DisplayName = "Update Product with Failure")]
        [Trait("ProductControllerTest", "Product Controller Tests")]
        public async Task UpdateWithFailure()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var viewModel = new GenerateDataTest().GetViewModelValid();
            var response = await client.PostAsync(hostApi,
                new StringContent(JsonConvert.SerializeObject(viewModel), System.Text.Encoding.UTF8, "application/json"));
            string json = await response.Content.ReadAsStringAsync();
            dynamic data = JObject.Parse(json.ToString());
            var product = JsonConvert.DeserializeObject<Product>(data.ToString());

            var responseGet = await client.GetAsync(hostApi + "/1");
            string jsonGet = await responseGet.Content.ReadAsStringAsync();
            dynamic dataGet = JObject.Parse(jsonGet.ToString());
            var productGet = JsonConvert.DeserializeObject<Product>(dataGet.ToString());

            var productUpdate = new GenerateDataTest().GetInvalid();
            var responsePut = await client.PutAsync(hostApi + "/-1",
                new StringContent(JsonConvert.SerializeObject(productUpdate), System.Text.Encoding.UTF8, "application/json"));
            string jsonPut = await responsePut.Content.ReadAsStringAsync();
            dynamic dataPut = JObject.Parse(jsonPut.ToString());
            var productPut = JsonConvert.DeserializeObject<Product>(dataPut.ToString());

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(product);
            Assert.Equal(viewModel.Name, product.Name);
            Assert.Equal(viewModel.Description, product.Description);
            Assert.Equal(((long)viewModel.Price), ((long)product.Price));
            Assert.Equal(HttpStatusCode.OK, responseGet.StatusCode);
            Assert.NotNull(productGet);
            Assert.Equal(productGet.Name, product.Name);
            Assert.Equal(productGet.Description, product.Description);
            Assert.Equal(((long)productGet.Price), ((long)product.Price));
            Assert.Equal(HttpStatusCode.BadRequest, responsePut.StatusCode);
            Assert.NotNull(productPut);
            Assert.NotEqual(productPut.Name, viewModel.Name);
            Assert.NotEqual(productPut.Description, product.Description);
            Assert.NotEqual(((long)productPut.Price), ((long)product.Price));
        }


        [Fact(DisplayName = "Delete Product with Success")]
        [Trait("ProductControllerTest", "Product Controller Tests")]
        public async Task DeleteWithSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var viewModel = new GenerateDataTest().GetViewModelValid();
            var response = await client.PostAsync(hostApi,
                new StringContent(JsonConvert.SerializeObject(viewModel), System.Text.Encoding.UTF8, "application/json"));
            string json = await response.Content.ReadAsStringAsync();
            dynamic data = JObject.Parse(json.ToString());
            var product = JsonConvert.DeserializeObject<Product>(data.ToString());

            var responseDelete = await client.DeleteAsync(hostApi + "/1");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(product);
            Assert.Equal(HttpStatusCode.OK, responseDelete.StatusCode);
        }

        [Fact(DisplayName = "Delete Product with Failure")]
        [Trait("ProductControllerTest", "Product Controller Tests")]
        public async Task DeleteWithFailure()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var responseDelete = await client.DeleteAsync(hostApi + "/-1");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, responseDelete.StatusCode);
        }
    }
}
