using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.Api.Controllers;
using RaceDay.Api.Data;
using RaceDay.Api.DTOs;
using RaceDay.Api.Models;
using Xunit;

namespace RaceDay.Tests
{
    public class AuthControllerTests
    {
        private RaceDayDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<RaceDayDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            
            var context = new RaceDayDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public async Task Register_NewUser_ReturnsOkResult()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var controller = new AuthController(context);

            var dto = new RegisterDto
            {
                Username = "TestRunner",
                Email = "test@raceday.com",
                Password = "SecurePassword123",
                RoleId = 2
            };

            // Act
            var result = await controller.Register(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
            
            var userInDb = await context.Users.FirstOrDefaultAsync(u => u.Email == "test@raceday.com");
            Assert.NotNull(userInDb);
            Assert.Equal("TestRunner", userInDb.Username);
        }

        [Fact]
        public async Task Login_InvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var controller = new AuthController(context);

            var dto = new LoginDto
            {
                Email = "nonexistent@raceday.com",
                Password = "WrongPassword"
            };

            // Act
            var result = await controller.Login(dto);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }
    }
}