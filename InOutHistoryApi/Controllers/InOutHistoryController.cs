using System;
using DomainHistory.Interfaces;
using HistoryApi.Interfaces;
using AutoMapper;
using Common.InOutHistoryDto;
using Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace InOutHistoryApi.Controllers
{
    [ApiController]
    [Route("history")]
    public class InOutHistoryController : ControllerBase
    {
		private readonly IHistoryUnitOfWork _unitOfWork;
		private readonly ILogger<InOutHistoryController> _logger;
		private readonly IMapper _mapper;
		private readonly IInOutHistoryService _historyService;

		public InOutHistoryController(IHistoryUnitOfWork unitOfWork, ILogger<InOutHistoryController> logger, IMapper mapper,
            IInOutHistoryService historyService)
		{
			_unitOfWork = unitOfWork;
			_logger = logger;
			_mapper = mapper;
			_historyService = historyService;
		}

        [Authorize(Roles = "Admin")]
        [HttpPost("")]
        public async Task<List<InOutHistoryDto>> GetInOutHistories(InOutHistoryRequest request)
		{
			return await _historyService.GetInOutHistories(request);
		}

        [HttpPost("add")]
        public async Task AddHistory(InOutHistoryRequest request)
		{
			_historyService.AddHistory(request);
		}
	}
}

