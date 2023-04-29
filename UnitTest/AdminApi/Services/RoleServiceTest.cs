using System;
using AdminApi.MappingProfiles;
using Common.AdminDto;

namespace UnitTest.AdminApi.Services
{
	public class RoleServiceTest
	{
        private Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();
        private Mock<ILogger<RoleService>> _logger = new Mock<ILogger<RoleService>>();
        private RoleService _roleService;

        [SetUp]
        public void SetUp()
        {
            var myProfile = new EntitiesToDtosMappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            InitData();

            _roleService = new RoleService(_unitOfWork.Object, _logger.Object, mapper);
        }

        private void InitData()
        {
            var roles = new List<Role>
            {
                new Role
                {
                    Id = new Guid("3d85e57b-89ef-47c4-a336-5a72d7073204"),
                    Name = "User"
                },
                new Role
                {
                    Id = new Guid("1202e0f8-0095-4cee-9145-ee9990c2ce0d"),
                    Name = "Admin"
                }
            }.AsQueryable<Role>;

            _unitOfWork.Setup(x => x.Role.Query()).Returns(roles);
            _unitOfWork.Setup(x => x.Role.Add(It.IsAny<Role>())).Verifiable();
        }

        [Test]
        [TestCase("User", false)]
        [TestCase("Manager", true)]
        public void CreateRole(string roleName, bool check)
        {
            var dto = new RoleDto
            {
                Name = roleName
            };

            var result = _roleService.CreateRole(dto);

            Assert.That(result, Is.EqualTo(check));
        }
    }
}

