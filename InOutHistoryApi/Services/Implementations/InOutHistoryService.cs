using System;
using Common.InOutHistoryDto;
using DomainHistory.Interfaces;
using HistoryApi.Interfaces;
using AutoMapper;
using Request;
using DomainHistory.Entities;
using Common;

namespace HistoryApi.Implementations
{
	public class InOutHistoryService : IInOutHistoryService
    {
		private readonly IHistoryUnitOfWork _unitOfWork;
		private readonly ILogger<InOutHistoryService> _logger;
		private readonly IMapper _mapper;

		public InOutHistoryService(IHistoryUnitOfWork unitOfWork, ILogger<InOutHistoryService> logger, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_logger = logger;
			_mapper = mapper;
		}

		public async Task<List<InOutHistoryDto>> GetInOutHistories(InOutHistoryRequest request)
		{
			try
			{
				var query = _unitOfWork.InOutHistory.Query();

				query = request.StartTime == null ? query :
					query.Where(w => w.Created >= request.StartTime);
				query = request.EndTime == null ? query :
					query.Where(w => w.Created <= request.EndTime);
				query = string.IsNullOrEmpty(request.DoorName) ? query :
					query.Where(w => w.DoorName == request.DoorName);
				query = string.IsNullOrEmpty(request.UserName) ? query :
					query.Where(w => w.UserName == request.UserName);
				query = string.IsNullOrEmpty(request.ActionStatusName) ? query :
					query.Where(w => w.ActionStatusName == request.ActionStatusName);

				var result = query.OrderByDescending(o => o.Created)
					.Select(s => new InOutHistoryDto
					{
						Id = s.Id,
						ActionStatusName = s.ActionStatusName,
						DoorName = s.DoorName,
						UserName = s.UserName,
						UserId = s.UserId,
						DoorId = s.DoorId,
						ActionStatusId = s.ActionStatusId,
						Created = s.Created
					}).ToList();

				return result;
			}
			catch(Exception ex)
			{
				_logger.LogError("Error when get list histoties: " + ex.Message);
				return new List<InOutHistoryDto>();
			}
		}

        public async Task AddHistory(InOutHistoryRequest request)
		{
			try
			{
				var history = new InOutHistory();
				history.Id = Guid.NewGuid();
				history.Created = DateTime.Now;
				history.UserId = request.UserId;
				history.DoorId = request.DoorId;
				history.ActionStatusId = request.ActionStatusId;
				history.UserName = request.UserName;
				history.DoorName = request.DoorName;
				history.ActionStatusName = request.ActionStatusName;

				_unitOfWork.InOutHistory.Add(history);
				_unitOfWork.Save();
            }
			catch(Exception ex)
			{
				_logger.LogError("Error when write log: " + ex.Message);
            }
		}
    }
}

