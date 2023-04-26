using System;
using Dto;
using Interfaces;

namespace Implementations
{
	public class DoorService : IDoorService
	{
		private readonly IUnitOfWork _unitOfWork;

		public DoorService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public bool ValidOpen(DoorDto doorDto)
		{
			var doorEntity = _unitOfWork.Door.GetById(doorDto.Id);

			return doorEntity != null;
		}
	}
}

