using System;
using Common.UserDto;
using Microsoft.AspNetCore.Mvc;
using Requests;
using UserApi.Controllers;
using UserApi.Interfaces;
using UserApi.MappingProfiles;

namespace UnitTest.UserApi.Controllers
{
	public class UserControllerTest
	{
		private UserController _userController;
		private Mock<IUserService> _userService = new Mock<IUserService>();
		[SetUp]
		public void SetUp()
		{
            var configuration = new MapperConfiguration(cfg => {
				cfg.AddProfile(new DtosToViewModelsMappingProfile());
				cfg.AddProfile(new EntitiesToDtosMappingProfile());
            });
            IMapper mapper = new Mapper(configuration);

			InitData();

			_userController = new UserController(_userService.Object, mapper);
        }

		private void InitData()
		{
		}

		[Test]
		[TestCase("myid", "mypassworD1995!", 200)]
		public void SignUpTest_ReturnOk(string userName, string passWord, int code)
		{
			_userService.Setup(x => x.SignUp(It.IsAny<UserInfoDto>()))
				.Returns(true);
            var request = new SignupRequest
			{
				UserName = userName,
                Password = passWord
            };

			var result = _userController.SignUp(request) as OkObjectResult;

			Assert.IsNotNull(result);
			Assert.That(code, Is.EqualTo(result.StatusCode));
        }

        [Test]
        [TestCase("myid", "shortpassword", 400)]
        public void SignUpTest_ReturnBadRequest(string userName, string passWord, int code)
        {
            _userService.Setup(x => x.SignUp(It.IsAny<UserInfoDto>()))
                .Returns(false);
            var request = new SignupRequest
            {
                UserName = userName,
                Password = passWord
            };

            var result = _userController.SignUp(request) as OkObjectResult;

            Assert.IsNull(result);
        }

        [Test]
        [TestCase("myid", "mypassworD1995!", 200)]
        public void SignInTest_ReturnJWT(string userName, string passWord, int code)
        {
            _userService.Setup(x => x.SignIn(It.IsAny<UserInfoDto>()))
                .Returns("ThisIsJWTToken");
            var request = new SigninRequest
            {
                UserName = userName,
                Password = passWord
            };

            var result = _userController.SignIn(request) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.That(code, Is.EqualTo(result.StatusCode));
        }

        [Test]
        [TestCase("myid", "shortpassword", 400)]
        public void SignInTest_ReturnBadRequest(string userName, string passWord, int code)
        {
            _userService.Setup(x => x.SignIn(It.IsAny<UserInfoDto>()))
                .Returns("");
            var request = new SigninRequest
            {
                UserName = userName,
                Password = passWord
            };

            var result = _userController.SignIn(request) as OkObjectResult;

            Assert.IsNull(result);
        }
    }
}

