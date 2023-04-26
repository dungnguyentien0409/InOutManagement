using System;
namespace ViewModels
{
	public class OpenDoorViewModel
	{
		public Guid Id { get; set; }
		public Guid DoorId { get; set; }
		public Guid UserId { get; set; }
		public string DoorName { get; set; }
		public string UserName { get; set; }
	}
}

