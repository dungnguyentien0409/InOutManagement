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
                    client.BaseAddress = new Uri(_config.GetValue<string>("Endpoints:HistoryApi"));
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                    
                    HttpResponseMessage response = await client.PostAsync("history/add", CreateContentRequest(dto));

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

        private FormUrlEncodedContent CreateContentRequest(TapDoorDto dto)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("DoorName", dto.DoorName),
                new KeyValuePair<string, string>("UserName", dto.UserName),
                new KeyValuePair<string, string>("ActionStatusName", dto.TapAction)
            });

            return formContent;
        }
    }
}

