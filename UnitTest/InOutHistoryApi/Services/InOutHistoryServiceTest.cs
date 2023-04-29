using System;
using HistoryApi.MappingProfiles;
using HistoryApi.Implementations;
using Request;

namespace UnitTest.InOutHistoryApi.Services
{
	public class InOutHistoryServiceTest
	{
        private Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();
        private Mock<ILogger<InOutHistoryService>> _logger = new Mock<ILogger<InOutHistoryService>>();
        private Mock<IConfiguration> _config;
        private InOutHistoryService _historyService;

        [SetUp]
        public void Setup()
        {
            var myProfile = new DtosToViewModelsMappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            _config = new Mock<IConfiguration>();

            InitData();

            _historyService = new InOutHistoryService(_unitOfWork.Object, _logger.Object, mapper);
        }

        private void InitData()
        {
            var histories = new List<InOutHistory>
            {
                new InOutHistory
                {
                    Id = new Guid("c1d80c7c-4f39-4dbb-a39a-282cbd6ecb91"),
                    UserName = "AdminUser",
                    DoorName = "FrontDoor",
                    ActionStatusName = "TAPIN",
                },
                new InOutHistory
                {
                    Id = new Guid("59c66064-139d-4362-bc6e-28e4b930950b"),
                    UserName = "NormalUser",
                    DoorName = "StorageDoor",
                    ActionStatusName = "FAILED_TAPIN"
                },
                new InOutHistory
                {
                    Id = new Guid("90a81095-2e2a-41a9-990d-9d91a7010f83"),
                    UserName = "AdminUser",
                    DoorName = "StorageDoor",
                    ActionStatusName = "TAPIN"
                },
                new InOutHistory
                {
                    Id = new Guid("d99b207a-3312-4126-9a2c-b6f61fbef400"),
                    UserName = "NormalUser",
                    DoorName = "FrontDoor",
                    ActionStatusName = "TAPIN"
                }
            }.AsQueryable<InOutHistory>;

            _unitOfWork.Setup(x => x.InOutHistory.Query()).Returns(histories);
            _unitOfWork.Setup(x => x.InOutHistory.Add(It.IsAny<InOutHistory>())).Verifiable();
        }

        [Test]
        [TestCase(null, null, null, 4)]
        [TestCase("FrontDoor", null, null, 2)]
        [TestCase(null, "NormalUser", null, 2)]
        [TestCase(null, null, "TAPIN", 3)]
        [TestCase("FrontDoor", "AdminUser", "TAPIN", 1)]
        public void GetInOutHistoriesTest(string? doorName, string? userName, string? actionName, int count)
        {
            var request = new InOutHistoryRequest
            {
                DoorName = doorName,
                UserName = userName,
                ActionStatusName = actionName
            };

            var result = _historyService.GetInOutHistories(request).Result.Count;

            Assert.That(result, Is.EqualTo(count));
        }

    }
}

