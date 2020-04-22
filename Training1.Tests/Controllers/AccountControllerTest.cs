using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Training1.Areas.Identity.Data;
using Training1.Authorization;
using Training1.Controllers;
using Training1.Models.ViewModels;
using Training1.Services.Interfaces;
using Training1.Tests.Mock;
using Training1.Tests.Mock.Identity;
using Training1.Tests.Mock.Services;
using Xunit;

namespace Training1.Tests.Controllers
{
    public class AccountControllerTest
    {
        private readonly MockAccountService _mockService;
        private readonly MockUserManager _mockUserManager;
        private readonly MockUserValidator _mockUserValidator;
        private readonly AccountsController _accountsController;
        private readonly AccountsController _accountsControllerNoRole;

        private readonly AppUser _testAppUser;
        private const string _idTestAppUser = "1";
        public AccountControllerTest()
        {
            _mockService = new MockAccountService();
            _mockUserManager = new MockUserManager();
            _mockUserValidator = new MockUserValidator();
            _accountsController = GetAccountsControllerAdmin(_mockService, _mockUserManager, _mockUserValidator);
            _accountsControllerNoRole = GetAccountsControllerNoRole(_mockService, _mockUserManager, _mockUserValidator);
            _testAppUser = new AppUser { Email = "joe@test.com", Id = _idTestAppUser, UserName = "Joe" };
        }

        [Fact]
        public async Task Index_ReturnView()
        {
            //Arrange
            _mockService.MockGetModelForAccountList();

            //Act
            var result = await _accountsController.Index(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<string>());

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<AccountListViewModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task Edit_ReturnChallengeResult()
        {
            //Arrange

            //Act
            var result = await _accountsControllerNoRole.Edit(It.IsAny<string>());

            //Assert
            var viewResult = Assert.IsType<ChallengeResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnView()
        {
            //Arrange
            _mockService.MockGetEditAccount();

            //Act
            var result = await _accountsController.Edit(It.IsAny<string>());

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<AccountEditViewModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task EditPost_ReturnViewModel_WhenModelNotValid()
        {
            // Arrange
            _accountsController.ModelState.AddModelError("error", "some error");

            // Act
            var result = await _accountsController.Edit(null, null, null, null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Task<AccountEditViewModel>>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task EditPost_ReturnViewModel_WhenUnknownId()
        {
            // Arrange
            _mockUserManager.MockFindByIdAsync(null);

            // Act
            var result = await _accountsController.Edit(null, null, null, null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Task<AccountEditViewModel>>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task EditPost_ReturnChallengeResult_WhenNotAuthorized()
        {
            // Arrange
            _mockUserManager.MockFindByIdAsync(_testAppUser);

            // Act
            var result = await _accountsControllerNoRole.Edit(null, null, null, null);

            // Assert
            Assert.IsType<ChallengeResult>(result);
        }

        [Fact]
        public async Task EditPost_ReturnViewResult_WhenEmailNotValid()
        {
            // Arrange
            _mockUserManager.MockFindByIdAsync(_testAppUser);
            IdentityResult unvalidEmailResult = IdentityResult.Failed(new IdentityError[0]);
            _mockUserValidator.MockValidateAsync(unvalidEmailResult);

            // Act
            var result = await _accountsController.Edit(_idTestAppUser, _testAppUser, null, null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Task<AccountEditViewModel>>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task EditPost_ReturnViewResult_WhenUpdateFailed()
        {
            // Arrange
            IdentityResult successResult = IdentityResult.Success;
            IdentityResult failedResult = IdentityResult.Failed(new IdentityError[0]);
            _mockUserValidator.MockValidateAsync(successResult);
            _mockUserManager.MockFindByIdAsync(_testAppUser)
                            .MockUpdateAsync(failedResult);

            // Act
            var result = await _accountsController.Edit(_idTestAppUser, _testAppUser, null, null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Task<AccountEditViewModel>>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task EditPost_ReturnToAction_WhenSucceed()
        {
            // Arrange
            IdentityResult successResult = IdentityResult.Success;
            _mockUserValidator.MockValidateAsync(successResult);
            _mockUserManager.MockFindByIdAsync(_testAppUser)
                            .MockUpdateAsync(successResult);

            // Act
            var result = await _accountsController.Edit(_idTestAppUser, _testAppUser, null, null);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Edit", redirectToActionResult.ActionName);
            _mockUserManager.Verify();
        }

        private AccountsController GetAccountsControllerAdmin(MockAccountService mockService, MockUserManager mockUserManager, MockUserValidator mockUserValidator)
        {
            var authService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IAccountService>(sp => mockService.Object);
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });

            var controller = new AccountsController(authService, mockUserManager.Object, mockUserValidator.Object, mockService.Object);
            MockAuthorizationService.SetupUserWithRole(controller, Constants.UserAdministratorsRole);

            return controller;
        }

        private AccountsController GetAccountsControllerNoRole(MockAccountService mockService, MockUserManager mockUserManager, MockUserValidator mockUserValidator)
        {
            var authService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IAccountService>(sp => mockService.Object);
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });

            var controller = new AccountsController(authService, mockUserManager.Object, mockUserValidator.Object, mockService.Object);

            return controller;
        }
    }
}
