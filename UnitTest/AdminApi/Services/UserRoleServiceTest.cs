using System;
using Entities;
using AdminApi.MappingProfiles;
using Common.AdminDto;
using AdminApi.Implementations;

namespace UnitTest.AdminApi.Services
{
	public class UserRoleServiceTest
	{
        private Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();
        private Mock<ILogger<UserRoleService>> _logger = new Mock<ILogger<UserRoleService>>();
        private UserRoleService _userRoleService;

        [SetUp]
        public void Setup()
        {
            var myProfile = new EntitiesToDtosMappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            InitData();

            _userRoleService = new UserRoleService(_unitOfWork.Object, _logger.Object);
        }

        private void InitData()
        {
            var users = new List<UserInfo>
            {
                new UserInfo
                {
                    Id = new Guid("3cb57aa7-5dab-43ad-ba38-f0b4a49d4be6"),
                    UserName = "NormalUser"
                },
                new UserInfo
                {
                    Id = new Guid("a40270c3-6040-4239-98a1-2f94d9d79351"),
                    UserName = "AdminUser"
                }
            }.AsQueryable<UserInfo>;
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
            var userRoles = new List<UserInfoRole>
            {
                new UserInfoRole
                {
                    Id = new Guid("0db80a40-ae96-44c0-a07a-722f63db35be"),
                    UserInfoId = new Guid("3cb57aa7-5dab-43ad-ba38-f0b4a49d4be6"),
                    RoleId = new Guid("3d85e57b-89ef-47c4-a336-5a72d7073204")
                },
                new UserInfoRole
                {
                    Id = new Guid("eeb99975-bd6e-4195-843b-cc96d8ff7391"),
                    UserInfoId = new Guid("a40270c3-6040-4239-98a1-2f94d9d79351"),
                    RoleId = new Guid("1202e0f8-0095-4cee-9145-ee9990c2ce0d")
                }
            }.AsQueryable<UserInfoRole>;

            _unitOfWork.Setup(x => x.UserInfo.Query()).Returns(users);
            _unitOfWork.Setup(x => x.Role.Query()).Returns(roles);
            _unitOfWork.Setup(x => x.UserInfoRole.Query()).Returns(userRoles);

            _unitOfWork.Setup(x => x.UserInfoRole.Add(It.IsAny<UserInfoRole>())).Verifiable();
        }

        [Test]
        [TestCase("NormalUser", "User", false)]
        [TestCase("AdminUser", "User", true)]
        [TestCase("NonExistUser", "User", false)]
        [TestCase("NormalUser", "NonExistRole", false)]
        public void AssignUserRoleTest(string userName, string roleName, bool check)
        {
            var dto = new UserInfoRoleDto
            {
                UserName = userName,
                RoleName = roleName
            };

            var result = _userRoleService.AssignRole(dto);

            Assert.That(result, Is.EqualTo(check));
        }

        [Test]
        [TestCase("NormalUser", "User", true)]
        [TestCase("NormalUser", "Admin", false)]
        [TestCase("NonExistUser", "User", false)]
        [TestCase("NormalUser", "NonExistRole", false)]
        public void DeassignUserRoleTest(string userName, string roleName, bool check)
        {
            var dto = new UserInfoRoleDto
            {
                UserName = userName,
                RoleName = roleName
            };

            var result = _userRoleService.DeassignRole(dto);

            Assert.That(result, Is.EqualTo(check));
        }
    }
}

