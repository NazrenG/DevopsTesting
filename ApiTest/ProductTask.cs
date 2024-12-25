using DevopsTesting.Dtos;
using Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System.Net.Http.Json;

namespace ApiTest
{
    [TestFixture]
    public class ProductTask
    {
        private WebApplicationFactory<Program> _factory;
        private  HttpClient _client;

        [SetUp]
        public void SetUp()
        {
            _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { builder.ConfigureServices(services => { }); }) ;
            _client=_factory.CreateClient();
        }

        [Test]
        public async Task GetProducts_ReturnsOkResponse()
        {
            //aragr
            var response = await _client.GetAsync("/api/product/AllProduct?top=10");
            response.EnsureSuccessStatusCode();
            var products=await response.Content.ReadFromJsonAsync<List<Product>>();
            Assert.That(products!=null);
        }

        [Test]
        public async Task GetProducts_ReturnCorrectProduct()
        {
            // Arrange
            var response = await _client.GetAsync("/api/product/AllProduct?top=1");
            var products = await response.Content.ReadFromJsonAsync<List<Product>>();
            var product = products?.FirstOrDefault();

            if (product != null)
            {
                // Act
                var returnProduct = await _client.GetAsync($"/api/product/{product.ProductID}");
                returnProduct.EnsureSuccessStatusCode();
                var productResult = await returnProduct.Content.ReadFromJsonAsync<Product>();

                // Assert
                Assert.That(productResult != null); 
            }
        }

        [Test]
        public async Task PostProducts_ReturnNewProduct()
        {
            //arrange
            var product=new AddProduct { Name="Defaul",Price=0};
            //act
            var response = await _client.PostAsJsonAsync("/api/product/AddProduct",product);
            //assert 
            response.EnsureSuccessStatusCode();
            var createdProduct = await response.Content.ReadFromJsonAsync<Product>();
            Assert.That(createdProduct != null); 
            Assert.That(product.Name, Is.EqualTo(createdProduct?.Name));
        }

        [Test]
        public async Task PutProduct_ReturnUpdatedProduct()
        {
            // Arrange
            var response = await _client.GetAsync("/api/product/AllProduct?top=1");
            response.EnsureSuccessStatusCode();

            var products = await response.Content.ReadFromJsonAsync<List<Product>>();
            var product = products?.FirstOrDefault();
            Assert.That(product, Is.Not.Null, "Product list is empty!");

            var existingProduct = product!;
            var updatedProduct = new AddProduct
            {
                Name = existingProduct.Name + " Updated",
                Price = existingProduct.Price + 10
            };

            // Act
            var updateResponse = await _client.PutAsJsonAsync($"/api/product/UpdatedProduct/{existingProduct.ProductID}", updatedProduct);
            updateResponse.EnsureSuccessStatusCode();

            var productResult = await updateResponse.Content.ReadFromJsonAsync<Product>();

            // Assert
            Assert.That(productResult, Is.Not.Null, "Updated product is null!");
             Assert.That(productResult.Name, Is.EqualTo(updatedProduct.Name), "Product name was not updated!");
            Assert.That(productResult.Price, Is.EqualTo(updatedProduct.Price), "Product price was not updated!");
        }


        [Test]
        public async Task DeleteProduct_ReturnDeletedProduct()
        {
            // Arrange
            var response = await _client.GetAsync("/api/product/AllProduct?top=1");
            response.EnsureSuccessStatusCode();

            var products = await response.Content.ReadFromJsonAsync<List<Product>>();
            var product = products?.FirstOrDefault();
            Assert.That(product, Is.Not.Null, "Product list is empty!");

            var existingProduct = product!;

            var deleteResponse = await _client.DeleteAsync($"/api/product/DeleteProduct/{existingProduct.ProductID}");
            deleteResponse.EnsureSuccessStatusCode();
             
        
            // check after delete
            var checkResponse = await _client.GetAsync($"/api/product/GetProduct/{existingProduct.ProductID}");
            Assert.That(checkResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound),
                "product dont delete to list!");
        }

    }
}
