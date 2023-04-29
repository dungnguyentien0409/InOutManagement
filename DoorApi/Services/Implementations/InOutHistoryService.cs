using System;
using Common.DoorDto;
using Domain.Interfaces;
using DoorApi.Interfaces;
using AutoMapper;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Request;
using Newtonsoft.Json;

namespace DoorApi.Implementations
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
                var actionResult = _unitOfWork.ActionStatus.Query()
                    .Where(w => w.Name == dto.TapAction)
                    .FirstOrDefault();

                if (actionResult == null)
                {
                    _logger.LogError("Action is not defined");
                    return;
                }

                var clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                var httpClient = new HttpClient(clientHandler);
                var url = _config.GetValue<string>("Endpoints:HistoryApi") + "history/add";
                var request = new InOutHistoryRequest();
                request.DoorName = dto.DoorName;
                request.UserName = dto.UserName;
                request.ActionStatusName = dto.TapAction;

                var response = httpClient.PostAsJsonAsync(url, request).Result;

                if (response == null || !response.IsSuccessStatusCode)
                {
                    _logger.LogError("Failt to write to history");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Issue when write to history: " + ex.Message);
            }
        }
    }
}

