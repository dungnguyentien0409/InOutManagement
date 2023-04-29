using System;
using DataAccessEF;
using Domain.Interfaces;
using DataAccessEF.UnitOfWork;
using Entities;
using Common.Helpers;
using Helper;

namespace Services
{
	public class DatabaseService
	{
		private readonly InOutManagementContext _context;
		private readonly IUnitOfWork _unitOfWork;

		public DatabaseService(InOutManagementContext context)
		{
			_context = context;
			_unitOfWork = new UnitOfWork(context);
		}

        public void CreateDefaultData()
        {
            CleanUpData();

            var dataHelper = new DataHelper(_unitOfWork);
            dataHelper.CreateDummyData();
        }


        private void CleanUpData()
        {
            _unitOfWork.InOutHistory.RemoveRange(_unitOfWork.InOutHistory.Query().ToList());
            _unitOfWork.ActionStatus.RemoveRange(_unitOfWork.ActionStatus.Query().ToList());
            _unitOfWork.Door.RemoveRange(_unitOfWork.Door.Query().ToList());
            _unitOfWork.UserInfo.RemoveRange(_unitOfWork.UserInfo.Query().ToList());
            _unitOfWork.DoorRole.RemoveRange(_unitOfWork.DoorRole.Query().ToList());
            _unitOfWork.UserInfoRole.RemoveRange(_unitOfWork.UserInfoRole.Query().ToList());
            _unitOfWork.Role.RemoveRange(_unitOfWork.Role.Query().ToList());
            _unitOfWork.Save();
        }
    }
}

