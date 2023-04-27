using System;
namespace Common.DoorDto
{
	public class InOutHistoryDto
	{
        public Guid? Id { get; set; }

        public Guid? DoorId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? ActionStatusId { get; set; }

        public string? DoorName { get; set; }
        public string? UserName { get; set; }
        public string? ActionStatusName { get; set; }

    }
}

