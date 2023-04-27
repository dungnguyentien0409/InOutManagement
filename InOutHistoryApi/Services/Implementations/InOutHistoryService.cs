using System;
using Common.InOutHistoryDto;
using Interfaces;
using AutoMapper;
using Request;
using Entities;

namespace Implementations
{
	public class InOutHistoryService : IInOutHistoryService
    {
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger<InOutHistoryService> _logger;
		private readonly IMapper _mapper;

		public InOutHistoryService(IUnitOfWork unitOfWork, ILogger<InOutHistoryService> logger, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_logger = logger;
			_mapper = mapper;
		}

		public async Task<List<InOutHistoryDto>> GetInOutHistories(InOutHistoryRequest request)
		{
            var (doorItem, userItem, actionStatusItem) = await GetReferenceData(request);
			var query = _unitOfWork.InOutHistory.Query();

			query = request.StartTime == null ? query :
				query.Where(w => w.Created >= request.StartTime);
			query = request.EndTime == null ? query :
				query.Where(w => w.Created <= request.EndTime);
			query = doorItem == null ? query :
				query.Where(w => w.DoorId == doorItem.Id);
			query = userItem == null ? query :
				query.Where(w => w.UserId == userItem.Id);
			query = actionStatusItem == null ? query :
				query.Where(w => w.ActionStatusId == actionStatusItem.Id);

			var result = query.Select(s => new InOutHistoryDto
			{
				Id = s.Id,
				ActionStatusId = s.ActionStatusId,
				DoorId = s.DoorId,
				UserId = s.UserId,
				Created = s.Created
			}).ToList();

			return result;
		}

        public async Task AddHistory(InOutHistoryRequest request)
		{
			var (doorItem, userItem, actionStatusItem) = await GetReferenceData(request);

			if (doorItem == null || userItem == null || actionStatusItem == null)
			{
				_logger.LogError("Reference data not existed");
				return;
			}

			try
			{
				var history = new InOutHistory();
				history.Id = Guid.NewGuid();
				history.Created = DateTime.Now;
				history.UserId = userItem.Id;
				history.DoorId = doorItem.Id;
				history.ActionStatusId = actionStatusItem.Id;

				_unitOfWork.InOutHistory.Add(history);
				_unitOfWork.Save();
			}
			catch(Exception ex)
			{
				_logger.LogError("Error when write log: " + ex.Message);
			}
		}

        private async Task<(Door?, UserInfo?, ActionStatus?)> GetReferenceData(InOutHistoryRequest request)
		{
			try
			{
				var doorItem = !string.IsNullOrEmpty(request.DoorName) ?
					_unitOfWork.Door.Find(w => w.Name == request.DoorName)
					.FirstOrDefault() : null;
				var userItem = !string.IsNullOrEmpty(request.UserName) ?
					_unitOfWork.UserInfo.Find(w => !string.IsNullOrEmpty(request.UserName) && w.UserName == request.UserName)
					.FirstOrDefault() : null;
				var actionStatusItem = !string.IsNullOrEmpty(request.ActionStatusName) ?
					_unitOfWork.ActionStatus.Find(w => w.Name == request.ActionStatusName)
					.FirstOrDefault() : null;

				return (doorItem, userItem, actionStatusItem);
			}
			catch(Exception ex) {
				_logger.LogError("Cannot get referece data: " + ex.Message);
				return (null, null, null);
			}
		}
    }
}

