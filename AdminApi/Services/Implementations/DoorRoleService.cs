using System;
using Common.AdminDto;
using Entities;
using Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Implementations
{
    [Authorize(Roles = "Admin")]
    public class DoorRoleService : IDoorRoleService
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DoorRoleService> _logger;
        private readonly IMapper _mapper;

		public DoorRoleService(IUnitOfWork unitOfWork, ILogger<DoorRoleService> logger, IMapper mapper)
		{
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
		}

        public bool AssignDoorRole(DoorRoleDto doorRoleDto)
        {
            try
            {
                var doorItem = _unitOfWork.Door.Find(f => f.Name == doorRoleDto.DoorName).FirstOrDefault();
                if (doorItem == null)
                {
                    _logger.LogError("This door did not existed");
                    return false;
                }
                var roleItem = _unitOfWork.Role.Find(f => f.Name == doorRoleDto.RoleName).FirstOrDefault();
                if (roleItem == null)
                {
                    _logger.LogError("This role did not existed");
                    return false;
                }
                var doorRoleItem = _unitOfWork.DoorRole
                    .Find(f => f.DoorId == doorItem.Id && f.RoleId == roleItem.Id)
                    .FirstOrDefault();
                if (doorRoleItem != null)
                {
                    _logger.LogError("This door role has already existed");
                    return true;
                }

                doorRoleItem = _mapper.Map<DoorRole>(doorRoleDto);

                _unitOfWork.DoorRole.Add(doorRoleItem);
                _unitOfWork.Save();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when assiging door role: " + ex.Message);
                return false;
            }
        }

        public bool DeassignDoorRole(DoorRoleDto doorRoleDto)
        {
            try
            {
                var doorItem = _unitOfWork.Door.Find(f => f.Name == doorRoleDto.DoorName).FirstOrDefault();
                if (doorItem == null)
                {
                    _logger.LogError("This door did not existed");
                    return false;
                }
                var roleItem = _unitOfWork.Role.Find(f => f.Name == doorRoleDto.RoleName).FirstOrDefault();
                if (roleItem == null)
                {
                    _logger.LogError("This role did not existed");
                    return false;
                }
                var doorRoleItem = _unitOfWork.DoorRole
                    .Find(f => f.DoorId == doorItem.Id && f.RoleId == roleItem.Id)
                    .FirstOrDefault();
                if (doorRoleItem == null)
                {
                    _logger.LogError("This door role did not existed");
                    return false;
                }

                _unitOfWork.DoorRole.Remove(doorRoleItem);
                _unitOfWork.Save();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when deassiging door role: " + ex.Message);
                return false;
            }
        }
    }
}

