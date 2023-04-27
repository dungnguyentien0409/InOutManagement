using System;
using Common.DoorDto;
using Interfaces;
using AutoMapper;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Request;
using Newtonsoft.Json;

namespace Implementations
{
	public class InOutHistoryService : IInOutHistoryService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger<InOutHistoryService> _logger;
		private readonly IMapper _mapper;
        private readonly IConfiguration _config;

		public InOutHistoryService(IUnitOfWork unitOfWork, ILogger<InOutHistoryService> logger, IMapper mapper,
            IConfiguration config)
		{
			_unitOfWork = unitOfWork;
			_logger = logger;
			_mapper = mapper;
            _config = config;
		}

        public async Task SaveToHistory(TapDoorDto dto)
		{
            try
            {
                var actionResult = _unitOfWork.ActionStatus.Find(f => f.Name == dto.TapAction)
                    .FirstOrDefault();

                if (actionResult == null)
                {
                    _logger.LogError("Action is not defined");
                    return;
                }


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://" + _config.GetValue<string>("Endpoints:HistoryApi") + "/");
                    client.DefaultRequestHeaders.Accept.Clear();

                    // New code:
                    var request = new InOutHistoryRequest();
                    request.DoorId = dto.DoorId;
                    request.UserId = dto.UserId;
                    request.ActionStatusId = actionResult.Id;
                    request.DoorName = dto.DoorName;
                    request.UserName = dto.UserName;
                    request.ActionStatusName = dto.TapAction;

                    var requestString = JsonConvert.SerializeObject(request);
                    HttpResponseMessage response = await client.PostAsJsonAsync("history/add", requestString);

                    if (!response.IsSuccessStatusCode)
                    {
                        _logger.LogError("Failt to write to history");
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Issue when write to history: " + ex.Message);
            }
        }
    }
}

