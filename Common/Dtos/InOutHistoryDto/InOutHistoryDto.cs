using System;
namespace Common.InOutHistoryDto
{
	public class InOutHistoryDto
	{
		public Guid Id { get; set; }
		public string UserName { get; set; }
		public string DoorName { get; set; }
		public string ActionStatusName { get; set; }
		public DateTime Created { get; set; }
	}
}

