using System;
using Interfaces;
using Dto;
using Entities;
using AutoMapper;

namespace Implementations
{
	public class RoleService : IRoleService
	{
        private readonly ILogger<RoleService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

		public RoleService(IUnitOfWork unitOfWork, ILogger<RoleService> logger, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_logger = logger;
			_mapper = mapper;
		}

        public bool CreateRole(RoleDto roleDto)
		{
			var roleItem = _unitOfWork.Role.Find(w => w.Name == roleDto.Name).FirstOrDefault();

			if (roleItem != null)
			{
				_logger.LogError("Role already existed");
				return false;
			}

			try
			{
				roleItem = _mapper.Map<Role>(roleDto);
				roleItem.Id = Guid.NewGuid();
				roleItem.Created = DateTime.Now;

				_unitOfWork.Role.Add(roleItem);
				_unitOfWork.Save();

				return true;
			}
			catch(Exception ex)
			{
				_logger.LogError("Error when creating new role: " + ex.Message);
				return false;
			}
		}

        public bool AssignRole(UserInfoRoleDto userInfoRoleDto)
		{
			try
			{
				var roleItem = _unitOfWork.Role.Find(f => f.Name == userInfoRoleDto.RoleName).FirstOrDefault();
				var userItem = _unitOfWork.UserInfo.Find(f => f.UserName == userInfoRoleDto.UserName).FirstOrDefault();

				if (roleItem == null)
				{
					_logger.LogError("Role not existed");
					return false;
				}
				if (userItem == null)
				{
					_logger.LogError("User not existed");
					return false;
				}

				var userRole = _unitOfWork.UserInfoRole.Find(f => f.RoleId == roleItem.Id && f.UserInfoId == userItem.Id)
					.FirstOrDefault();

				if (userRole != null)
				{
					_logger.LogError("User has already had this role");
				}

				userRole = new Entities.UserInfoRole();
				userRole.Id = Guid.NewGuid();
				userRole.Created = DateTime.Now;
				userRole.RoleId = roleItem.Id;
				userRole.UserInfoId = userItem.Id;

				_unitOfWork.UserInfoRole.Add(userRole);
				_unitOfWork.Save();

				return true;
			}
			catch(Exception ex)
			{
				_logger.LogError("Cannot assign role for this user: " + ex.Message);
				return false;
			}
		}

        public bool DeassignRole(UserInfoRoleDto userInfoRoleDto)
        {
            try
            {
                var roleItem = _unitOfWork.Role.Find(f => f.Name == userInfoRoleDto.RoleName).FirstOrDefault();
                var userItem = _unitOfWork.UserInfo.Find(f => f.UserName == userInfoRoleDto.UserName).FirstOrDefault();

                if (roleItem == null)
                {
                    _logger.LogError("Role not existed");
                    return false;
                }
                if (userItem == null)
                {
                    _logger.LogError("User not existed");
                    return false;
                }

                var userRole = _unitOfWork.UserInfoRole.Find(f => f.RoleId == roleItem.Id && f.UserInfoId == userItem.Id)
                    .FirstOrDefault();

                if (userRole == null)
                {
                    _logger.LogError("User does not have this role");
                }

                _unitOfWork.UserInfoRole.Remove(userRole);
                _unitOfWork.Save();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Cannot assign role for this user: " + ex.Message);
                return false;
            }
        }
    }
}

