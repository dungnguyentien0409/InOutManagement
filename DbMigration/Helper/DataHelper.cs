using System;
using Common.Helpers;
using Entities;
using Interfaces;

namespace Helper
{
	public class DataHelper
	{
		private readonly IUnitOfWork _unitOfWork;

		public DataHelper(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
            CreateActionStatus();
        }

        public void CreateDummyData()
        {
            var userNames = new string[] { "Normal User", "Admin User" };
            var roleNames = new string[] { "User", "Admin" };
            var doorNames = new string[] { "Front Door", "Storage" };

            CreateDummyData(userNames, doorNames, roleNames);
		}

        private void CreateDummyData(string[] userNames, string[] doorNames, string[] roleNames)
        {
            for (var i = 0; i < userNames.Length; i++)
            {
                var userId = CreateUser(userNames[i]);
                var doorId = CreateDoor(doorNames[i]);
                var roleId = CreateRole(roleNames[i]);
                
                if (roleId.HasValue && userId.HasValue)
                {
                    CreateUserRole(userId.Value, roleId.Value);
                }
                if (roleId.HasValue && userId.HasValue)
                {
                    CreateDoorRole(doorId.Value, roleId.Value);
                }
            }
        }

        private Guid? CreateRole(string roleName)
        {
            try
            {
                var adminRole = new Role();
                adminRole.Id = Guid.NewGuid();
                adminRole.Name = roleName;
                adminRole.Created = DateTime.Now;

                _unitOfWork.Role.Add(adminRole);
                _unitOfWork.Save();

                return adminRole.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error when creating admin role: " + ex.Message);

                return null;
            }
        }

        private Guid? CreateUser(string userName)
        {
            try
            {
                var user = new UserInfo();
                user.Id = Guid.NewGuid();
                user.Created = DateTime.Now;
                user.UserName = userName;
                user.Email = userName;
                user.Salt = PasswordHelper.GenerateSalt(256);
                user.HashedPassword = PasswordHelper.HashPassword(userName, user.Salt);

                _unitOfWork.UserInfo.Add(user);
                _unitOfWork.Save();

                return user.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error when creating user: " + ex.Message);

                return null;
            }
        }

        private Guid? CreateUserRole(Guid UserId, Guid RoleId)
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
            catch (Exception ex)
            {
                Console.WriteLine("Error when creating user role: " + ex.Message);

                return null;
            }
        }

        private void CreateActionStatus()
        {
            try
            {
                var actions = new string[] { "TAPIN", "TAPOUT", "FAILED_TAPIN", "FAILED_TAPOUT" };
                var actionStatusItems = new List<ActionStatus>();

                foreach (var action in actions)
                {
                    actionStatusItems.Add(new ActionStatus
                    {
                        Id = Guid.NewGuid(),
                        Created = DateTime.Now,
                        Name = action
                    });
                }

                _unitOfWork.ActionStatus.AddRange(actionStatusItems);
                _unitOfWork.Save();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error when creating action status: " + ex.Message);
            }
        }

        private Guid? CreateDoor(string doorName)
        {
            try
            {
                var door = new Door();
                door.Id = Guid.NewGuid();
                door.Name = doorName;
                door.Description = doorName;
                door.Created = DateTime.Now;

                _unitOfWork.Door.Add(door);
                _unitOfWork.Save();

                return door.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error when creating door: " + ex.Message);

                return null;
            }
        }

        private Guid? CreateDoorRole(Guid DoorId, Guid RoleId)
        {
            try
            {
                var doorRole = new DoorRole();
                doorRole.Id = Guid.NewGuid();
                doorRole.Created = DateTime.Now;
                doorRole.RoleId = RoleId;
                doorRole.DoorId = DoorId;

                _unitOfWork.DoorRole.Add(doorRole);
                _unitOfWork.Save();

                return doorRole.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error when creating door role: " + ex.Message);

                return null;
            }
        }
    }
}

