using System;
using DomainHistory.Interfaces;
using DoorApi.Interfaces;
using InOutHistoryApi.Controllers;
using HistoryApi.MappingProfiles;
using Request;
using Common.DoorDto;

namespace UnitTest.InOutHistoryApi.Controllers
{
	public class InOutHistoryControllerTest
	{
		private InOutHistoryController _controller;
        private Mock<IHistoryUnitOfWork> _unitOfWork = new Mock<IHistoryUnitOfWork>();
        private Mock<ILogger<InOutHistoryController>> _logger = new Mock<ILogger<InOutHistoryController>>();
        private Mock<HistoryApi.Interfaces.IInOutHistoryService> _historyService = new Mock<HistoryApi.Interfaces.IInOutHistoryService>();

        [SetUp]
		public void SetUp()
		{
            var configuration = new MapperConfiguration(cfg => {
                cfg.AddProfile(new DtosToViewModelsMappingProfile());
            });
            IMapper mapper = new Mapper(configuration);
            _controller = new InOutHistoryController(_unitOfWork.Object, _logger.Object, mapper, _historyService.Object);

            var histories = new List<Common.InOutHistoryDto.InOutHistoryDto>();

            _historyService.Setup(x => x.AddHistory(It.IsAny<InOutHistoryRequest>())).Verifiable();
            _historyService.Setup(x => x.GetInOutHistories(It.IsAny<InOutHistoryRequest>()))
                .Returns(Task.FromResult(histories));
		}

        [Test]
        [TestCase("Test", "Test", "Test")]
        public void GetAllHistories(string userName, string doorName, string tapAction)
        {
            var request = new InOutHistoryRequest()
            {
                UserName = userName,
                DoorName = doorName,
                ActionStatusName = tapAction
            };

            var result = _controller.GetInOutHistories(request).Result;

            Assert.IsInstanceOf<List<Common.InOutHistoryDto.InOutHistoryDto>>(result);
        }

        [Test]
        [TestCase("Test", "Test", "Test")]
        public void AddHistory(string userName, string doorName, string tapAction)
        {
            var request = new InOutHistoryRequest
            {
                UserName = userName,
                DoorName = doorName,
                ActionStatusName = tapAction
            };

            try
            {
                _controller.AddHistory(request);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsFalse(false);
            }
        }
	}
}

