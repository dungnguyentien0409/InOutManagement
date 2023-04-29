using System;
using Common.AdminDto;
using Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Implementations
{
    [Authorize(Roles = "Admin")]
    public class UserRoleService : IUserRoleService
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserRoleService> _logger;

		public UserRoleService(IUnitOfWork unitOfWork, ILogger<UserRoleService> logger)
		{
            _unitOfWork = unitOfWork;
            _logger = logger;
		}

        public bool AssignRole(UserInfoRoleDto userInfoRoleDto)
        {
            try
            {
                var roleItem = _unitOfWork.Role.Query()
                    .Where(w => w.Name == userInfoRoleDto.RoleName).FirstOrDefault();
                var userItem = _unitOfWork.UserInfo.Query()
                    .Where(w => w.UserName == userInfoRoleDto.UserName).FirstOrDefault();

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

                var userRole = _unitOfWork.UserInfoRole.Query()
                    .Where(w => w.RoleId == roleItem.Id && w.UserInfoId == userItem.Id)
                    .FirstOrDefault();

                if (userRole != null)
                {
                    _logger.LogError("User has already had this role");
                    return false;
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
            catch (Exception ex)
            {
                _logger.LogError("Cannot assign role for this user: " + ex.Message);
                return false;
            }
        }

        public bool DeassignRole(UserInfoRoleDto userInfoRoleDto)
        {
            try
            {
                var roleItem = _unitOfWork.Role.Query()
                    .Where(w => w.Name == userInfoRoleDto.RoleName).FirstOrDefault();
                var userItem = _unitOfWork.UserInfo.Query()
                    .Where(w => w.UserName == userInfoRoleDto.UserName).FirstOrDefault();

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

                var userRole = _unitOfWork.UserInfoRole.Query()
                    .Where(f => f.RoleId == roleItem.Id && f.UserInfoId == userItem.Id)
                    .FirstOrDefault();

                if (userRole == null)
                {
                    _logger.LogError("User does not have this role");
                    return false;
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

