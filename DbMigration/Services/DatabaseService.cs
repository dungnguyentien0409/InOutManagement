using System;
using DataAccessEF;
using Interfaces;
using Entities;
using Common.Helpers;

namespace Services
{
	public class DatabaseService
	{
		private readonly InOutManagementContext _context;
		private readonly IUnitOfWork _unitOfWork;

		public DatabaseService(InOutManagementContext context)
		{
			_context = context;
			_unitOfWork = new UnitOfWork.UnitOfWork(context);
		}

        public void CreateDefaultData()
        {
            CleanUpData();

            var roleId = CreateRoleAdmin();
            if (roleId == null)
            {
                return;
            }
            var userId = CreateUserAdmin();
            if (userId == null)
            {
                return;
            }

            CreateUserRole(roleId.Value, userId.Value);
        }

        private Guid? CreateRoleAdmin()
        {
            try
            {
                var adminRole = new Role();
                adminRole.Id = Guid.NewGuid();
                adminRole.Name = "Admin";
                adminRole.Created = DateTime.Now;

                return adminRole.Id;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error when creating admin role: " + ex.Message);

                return null;
            }
        }

        private Guid? CreateUserAdmin()
        {
            try
            {
                var user = new UserInfo();
                user.Id = Guid.NewGuid();
                user.Created = DateTime.Now;
                user.UserName = "Admin";
                user.Email = "Admin";
                user.Salt = PasswordHelper.GenerateSalt(256);
                user.HashedPassword = PasswordHelper.HashPassword("Admin", user.Salt);

                _unitOfWork.UserInfo.Add(user);
                _unitOfWork.Save();

                return user.Id;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error when creating user: " + ex.Message);

                return null;
            }
        }

        private Guid? CreateUserRole(Guid RoleId, Guid UserId)
        {
            try
            {
                var userRole = new UserInfoRole();
                userRole.Id = Guid.NewGuid();
                userRole.Created = DateTime.Now;
                userRole.RoleId = RoleId;
                userRole.UserInfoId = UserId;

                _unitOfWork.UserInfoRole.Add(userRole);
                _unitOfWork.Save();

                return userRole.Id;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error when creating user role: " + ex.Message);

                return null;
            }
        }

        private void CleanUpData()
        {
            _unitOfWork.Door.RemoveRange(_unitOfWork.Door.Query().ToList());
            _unitOfWork.UserInfo.RemoveRange(_unitOfWork.UserInfo.Query().ToList());
            _unitOfWork.DoorRole.RemoveRange(_unitOfWork.DoorRole.Query().ToList());
            _unitOfWork.UserInfoRole.RemoveRange(_unitOfWork.UserInfoRole.Query().ToList());
            _unitOfWork.Role.RemoveRange(_unitOfWork.Role.Query().ToList());
            _unitOfWork.Save();
        }
    }
}

