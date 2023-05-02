using System;
using AdminApi.Interfaces;
using UserApi.Controllers;
using AdminApi.MappingProfiles;
using Requests;
using Common.AdminDto;

namespace UnitTest.AdminApi.Controllers
{
	public class RoleControllerTest
	{
		private Mock<IRoleService> _roleService = new Mock<IRoleService>();
		private RoleController _controller;

		[SetUp]
		public void SetUp()
		{
            var configuration = new MapperConfiguration(cfg => {
                cfg.AddProfile(new DtosToViewModelsMappingProfile());
                cfg.AddProfile(new EntitiesToDtosMappingProfile());
            });
            IMapper mapper = new Mapper(configuration);

            _controller = new RoleController(_roleService.Object, mapper);

            _roleService.Setup(x => x.CreateRole(It.IsAny<RoleDto>()))
                .Returns(true);
        }

        [Test]
        [TestCase("TestRole")]
        public void CreateRoleTest(string roleName)
        {
            var request = new RoleRequest
            {
                Name = roleName
            };

            var res = _controller.CreateRole(request);

            Assert.That(res, Is.EqualTo(true));
        }
    }
}

