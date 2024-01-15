using Application.DTOs.Requests;
using Infrastructure.DAL;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace IntegrationTests
{
    public class AuthControllerTests
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public AuthControllerTests()
        {
            _factory = new();
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task GetUsers_ShouldReturnOk()
        {
            var response = await _client.GetAsync("https://localhost:7295/api/Auth/GetUsers");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CreateUser_ShouldReturnOk()
        {
            var httpContent = new StringContent(JsonConvert.SerializeObject(new CreateUserRequest()
            {
                Name = "admin",
                SurName = "admin",
                LastName = "admin",
                Post = "Администратор",
                Login = "admin",
                Password = "admin"
            }),
                Encoding.UTF8, "application/json"); ;
            var response = await _client.PostAsync("https://localhost:7295/api/Auth/CreateUser", httpContent);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Login_ShouldReturnOk()
        {
            var httpContent = new StringContent(JsonConvert.SerializeObject(new LoginRequest() { Login = "admin", Password = "admin" }),
                Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("https://localhost:7295/api/Auth/auth", httpContent);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteUserById_ShouldReturnOk()
        {
            var response = await _client.DeleteAsync("https://localhost:7295/api/Auth/DeleteUser/4");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

    }
}
