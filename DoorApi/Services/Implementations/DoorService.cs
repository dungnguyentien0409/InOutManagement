using System;
using Interfaces;
using Entities;
using AutoMapper;
using Common.Door.Dto;

namespace Implementations
{
	public class DoorService : IDoorService
	{
		private ILogger<DoorService> _logger;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public DoorService(IUnitOfWork unitOfWork, ILogger<DoorService> logger, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_logger = logger;
            _mapper = mapper;
		}

		public bool ValidOpen(DoorDto doorDto)
		{
			var doorEntity = _unitOfWork.Door.GetById(doorDto.Id);

			return doorEntity != null;
		}

		public bool CreateDoor(DoorDto doorDto)
		{
			try
			{
				var doorItem = _unitOfWork.Door.Find(f => f.Name == doorDto.Name).FirstOrDefault();

				if (doorItem != null)
				{
                    _logger.LogError("This door has already existed");
                    return false;
                }

				doorItem = _mapper.Map<Door>(doorDto);
				doorItem.Id = Guid.NewGuid();
				doorItem.Created = DateTime.Now;

				_unitOfWork.Door.Add(doorItem);
				_unitOfWork.Save();

				return true;
			}
			catch(Exception ex)
			{
				_logger.LogError("Error when creating new door: " + ex.Message);
				return false;
			}
		}
    }
}

