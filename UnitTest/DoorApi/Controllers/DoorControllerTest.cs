using System;
using DoorApi.Controllers;
using Domain.Interfaces;
using DoorApi.Interfaces;
using DoorApi.MappingProfiles;
using Common.DoorDto;
using Requests;

namespace UnitTest.DoorApi.Controllers
{
	public class DoorControllerTest
	{
		private DoorController _doorController;
		private Mock<IDoorService> _doorService = new Mock<IDoorService>();
        private Mock<IIotGatewayService> _iotGatewayService = new Mock<IIotGatewayService>();
        private Mock<IInOutHistoryService> _historyService = new Mock<IInOutHistoryService>();
		private Mock<ILogger<DoorController>> _logger = new Mock<ILogger<DoorController>>();

        [SetUp]
		public void SetUp()
		{
            var configuration = new MapperConfiguration(cfg => {
                cfg.AddProfile(new DtosToViewModelsMappingProfile());
                cfg.AddProfile(new EntitiesToDtosMappingProfile());
            });
            IMapper mapper = new Mapper(configuration);

            _doorController = new DoorController(_doorService.Object,
                _iotGatewayService.Object, mapper, _logger.Object, _historyService.Object);

            _iotGatewayService.Setup(x => x.SendDoorStatus(It.IsAny<TapDoorDto>())).Verifiable();
            _historyService.Setup(x => x.SaveToHistory(It.IsAny<TapDoorDto>())).Verifiable();

            _doorService.SetupSequence(x => x.Open(It.IsAny<TapDoorDto>()))
                .Returns(Task.FromResult(true))
                .Returns(Task.FromResult(false));

            _doorService.Setup(x => x.CreateDoor(It.IsAny<DoorDto>())).Returns(true);
        }

        [Test]
        [TestCase("Test", "Test", "Test", true)]
        [TestCase("Test2", "Test2", "Test2", false)]
        public void OpenDoorTest(string userName, string doorName, string tapAction, bool result)
        {
            var request = new TapDoorRequest()
            {
                UserName = userName,
                DoorName = doorName,
                TapAction = tapAction
            };

            var res = _doorController.Open(request).Result;

            Assert.That(result, Is.EqualTo(result));
        }

        [Test]
        public void CreateDoor()
        {
            var request = new DoorRequest()
            {
                Name = "TestDoor",
                Description = "Test Door"
            };

            var result = _doorController.Create(request);

            Assert.That(result, Is.EqualTo(true));
        }
	}
}

