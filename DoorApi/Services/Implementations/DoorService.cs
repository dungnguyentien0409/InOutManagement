using System;
using Interfaces;
using Entities;
using AutoMapper;
using Common.DoorDto;

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

		public async Task<bool> Open(TapDoorDto dto)
		{
            var doorItem = _unitOfWork.Door.Query()
				.Where(w => w.Name == dto.DoorName).FirstOrDefault();
            if (doorItem == null)
            {
                _logger.LogError("Door not existed");
                return false;
            }
            var userItem = _unitOfWork.UserInfo.Query()
				.Where(w => w.UserName == dto.UserName).FirstOrDefault();
            if (userItem == null)
            {
                _logger.LogError("User not existed");
                return false;
            }
            var userRoles = _unitOfWork.UserInfoRole.Query()
                .Where(w => w.UserInfoId == userItem.Id)
                .Select(s => s.RoleId).ToList();
            var doorRoles = _unitOfWork.DoorRole.Query()
                .Where(w => w.DoorId == doorItem.Id)
                .Select(s => s.RoleId).ToList();

            if (!doorRoles.Intersect(userRoles).Any())
            {
                _logger.LogError("User does not have access right to open the door");
                return false;
            }

            return true;
        }

		public bool CreateDoor(DoorDto doorDto)
		{
			try
			{
				var doorItem = _unitOfWork.Door.Query()
					.Where(w => w.Name == doorDto.Name).FirstOrDefault();

				if (doorItem != null)
				{
                    _logger.LogError("This door has already existed");
                    return false;
                }

				doorItem = _mapper.Map<Door>(doorDto);
				doorItem.Id = Guid.NewGuid();
				doorItem.Created = DateTime.Now;
				doorItem.Description = doorDto.Description;
				doorItem.Name = doorDto.Name;

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

