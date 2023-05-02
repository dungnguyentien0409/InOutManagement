using System;
using AdminApi.Controllers;
using AdminApi.Interfaces;
using AdminApi.MappingProfiles;
using Requests;

namespace UnitTest.AdminApi.Controllers
{
	public class DoorRoleControllerTest
	{
        private DoorRoleController _controller;
        private Mock<IMapper> _mapper = new Mock<IMapper>();
        private Mock<ILogger<DoorRoleController>> _logger = new Mock<ILogger<DoorRoleController>>();
        private Mock<IDoorRoleService> _doorRoleService = new Mock<IDoorRoleService>();

        [SetUp]
        public void SetUp()
        {
            var configuration = new MapperConfiguration(cfg => {
                cfg.AddProfile(new DtosToViewModelsMappingProfile());
                cfg.AddProfile(new EntitiesToDtosMappingProfile());
            });
            IMapper mapper = new Mapper(configuration);

            _controller = new DoorRoleController(mapper, _logger.Object, _doorRoleService.Object);
        }

        [Test]
        [TestCase("DoorTest", "RoleTest", true)]
        public void AssignTest_ReturnTrue(string doorName, string roleName, bool check)
        {
            _doorRoleService.Setup(x => x.AssignDoorRole(It.IsAny<Common.AdminDto.DoorRoleDto>()))
                .Returns(true);

            var request = new DoorRoleRequest
            {
                DoorName = doorName,
                RoleName = roleName
            };

            var result = _controller.AssignRole(request);

            Assert.That(result, Is.EqualTo(check));
        }

        [Test]
        [TestCase("DoorTest2", "RoleTest2", false)]
        public void AssignTest_ReturnFalse(string doorName, string roleName, bool check)
        {
            _doorRoleService.Setup(x => x.AssignDoorRole(It.IsAny<Common.AdminDto.DoorRoleDto>()))
                .Returns(false);

            var request = new DoorRoleRequest
            {
                DoorName = doorName,
                RoleName = roleName
            };

            var result = _controller.AssignRole(request);

            Assert.That(result, Is.EqualTo(check));
        }

        [Test]
        [TestCase("DoorTest", "RoleTest", true)]
        public void DeAssignTest_ReturnTrue(string doorName, string roleName, bool check)
        {
            _doorRoleService.Setup(x => x.DeassignDoorRole(It.IsAny<Common.AdminDto.DoorRoleDto>()))
                .Returns(true);

            var request = new DoorRoleRequest
            {
                DoorName = doorName,
                RoleName = roleName
            };

            var result = _controller.DeassignRole(request);

            Assert.That(result, Is.EqualTo(check));
        }

        [Test]
        [TestCase("DoorTest2", "RoleTest2", false)]
        public void DeAssignTest_ReturnFalse(string doorName, string roleName, bool check)
        {
            _doorRoleService.Setup(x => x.DeassignDoorRole(It.IsAny<Common.AdminDto.DoorRoleDto>()))
                .Returns(false);

            var request = new DoorRoleRequest
            {
                DoorName = doorName,
                RoleName = roleName
            };

            var result = _controller.DeassignRole(request);

            Assert.That(result, Is.EqualTo(check));
        }
    }
}

