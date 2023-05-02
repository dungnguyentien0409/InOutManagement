using System;
using AdminApi.Controllers;
using AdminApi.Implementations;
using AdminApi.Interfaces;
using AdminApi.MappingProfiles;
using Requests;

namespace UnitTest.AdminApi.Controllers
{
	public class UserRoleControllerTest
	{
        private Mock<ILogger<UserRoleController>> _logger = new Mock<ILogger<UserRoleController>>();
        private Mock<IMapper> _mapper = new Mock<IMapper>();
        private Mock<IUserRoleService> _userRoleService = new Mock<IUserRoleService>();
        private UserRoleController _controller;

        [SetUp]
        public void SetUp()
        {
            var configuration = new MapperConfiguration(cfg => {
                cfg.AddProfile(new DtosToViewModelsMappingProfile());
                cfg.AddProfile(new EntitiesToDtosMappingProfile());
            });
            IMapper mapper = new Mapper(configuration);
            _controller = new UserRoleController(_logger.Object, mapper, _userRoleService.Object);
        }

        [Test]
        [TestCase("UserTest", "RoleTest", true)]
        public void AssignTest_ReturnTrue(string userName, string roleName, bool check)
        {
            _userRoleService.Setup(x => x.AssignRole(It.IsAny<Common.AdminDto.UserInfoRoleDto>()))
                .Returns(true);
            var request = new UserInfoRoleRequest
            {
                UserName = userName,
                RoleName = roleName
            };

            var result = _controller.AssignRole(request);

            Assert.That(result, Is.EqualTo(check));
        }

        [Test]
        [TestCase("UserTest2", "RoleTest2", false)]
        public void AssignTest_ReturnFalse(string userName, string roleName, bool check)
        {
            _userRoleService.Setup(x => x.AssignRole(It.IsAny<Common.AdminDto.UserInfoRoleDto>()))
                .Returns(false);

            var request = new UserInfoRoleRequest
            {
                UserName = userName,
                RoleName = roleName
            };

            var result = _controller.AssignRole(request);

            Assert.That(result, Is.EqualTo(check));
        }

        [Test]
        [TestCase("UserTest", "RoleTest", true)]
        public void DeAssignTest_ReturnTrue(string userName, string roleName, bool check)
        {
            _userRoleService.Setup(x => x.DeassignRole(It.IsAny<Common.AdminDto.UserInfoRoleDto>()))
                .Returns(true);

            var request = new UserInfoRoleRequest
            {
                UserName = userName,
                RoleName = roleName
            };

            var result = _controller.DeassignRole(request);

            Assert.That(result, Is.EqualTo(check));
        }

        [Test]
        [TestCase("UserTest2", "RoleTest2", false)]
        public void DeAssignTest_ReturnFalse(string userName, string roleName, bool check)
        {
            _userRoleService.Setup(x => x.DeassignRole(It.IsAny<Common.AdminDto.UserInfoRoleDto>()))
                .Returns(false);

            var request = new UserInfoRoleRequest
            {
                UserName = userName,
                RoleName = roleName
            };

            var result = _controller.DeassignRole(request);

            Assert.That(result, Is.EqualTo(check));
        }
    }
}

