using Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ApiTest
{
    [TestFixture]
    public class ProductTask
    {
        private WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        [SetUp]
        public void SetUp()
        {
            _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { builder.ConfigureServices(services => { }); }) ;
            _client=new HttpClient();
        }

        [Test]
        public async Task GetProducts_ReturnsOkResponse()
        {
            //aragr
            var response = await _client.GetAsync("/api/product?top=10");
            response.EnsureSuccessStatusCode();
            var products=await response.Content.ReadFromJsonAsync<List<Product>>();
            Assert.That(products!=null);
        }

        [Test]
        public async Task GetProducts_ReturnCorrectProduct()
        {
            //aragr
            var response = await _client.GetAsync("/api/product?top=1");
            var products=await response.Content.ReadFromJsonAsync<List<Product>>();
            var product=products.FirstOrDefault();
            if (product != null)
            {
                var returnProduct = await _client.GetAsync($"/api/product/{product.İd}");
                Assert.That(returnProduct != null);
                var productResult = await returnProduct.Content.ReadFromJsonAsync<List<Product>>();
                Assert.That(productResult != null);
                Assert.That(productResult?.İd, Is.EqualTo(product.İd));   
            }
     
        }
    }
}
