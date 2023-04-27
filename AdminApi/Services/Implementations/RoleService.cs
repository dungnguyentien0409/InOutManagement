using System;
using Interfaces;
using Common.AdminDto;
using Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Implementations
{
    [Authorize(Roles = "Admin")]
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
    }
}

