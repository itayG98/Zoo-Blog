using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Model.DAL;
using Moq;

namespace ModelTests
{
    [Ignore("No suitable set Up of IdentityContext userManager and SignInMananager")]
    public class IdentityContextTest
    {
        private ZooIdentityContext _identityContext;
        private readonly string? _connString;
        private UserManager<IdentityUser>? _userManager;
        private SignInManager<IdentityContextTest>? _signInManager;
        private const string PASSWORD = "1234ABcd@";
        private readonly IdentityUser user = new IdentityUser
        {
            UserName = "Test",
            PhoneNumber = "1234565456",
            Email = "Test"
        };

        [SetUp]
        [Ignore("No suitable set Up of IdentityContext userManager and SignInMananager")]
        public void Setup()
        {
            string _connString = "Data Source=IdentityMockDB.db;";
            var optionsBuilder = new DbContextOptionsBuilder<ZooIdentityContext>();
            optionsBuilder.UseSqlite(_connString);
            _identityContext = new ZooIdentityContext(optionsBuilder.Options);
            if (_identityContext != default)
            {
                _identityContext.Database.EnsureDeleted();
                _identityContext.Database.EnsureCreated();
                var _userManager = new Mock<UserManager<IdentityUser>>();
                var _signInManager = new Mock<SignInManager<IdentityContextTest>>();
            }
            else
                throw new Exception("Repositories were not set");
        }

        [Test]
        [Ignore("No suitable set Up of IdentityContext userManager and SignInMananager")]
        public async Task Register()
        {
            var createResult = await _userManager.CreateAsync(user, PASSWORD);
            var addRole = await _userManager.AddToRoleAsync(user, "User");
            var signUpResult = await _signInManager.PasswordSignInAsync(user.UserName, PASSWORD, false, false);
            Assert.IsTrue(addRole.Succeeded);  
            Assert.IsTrue(signUpResult.Succeeded);
        }
    }
}

