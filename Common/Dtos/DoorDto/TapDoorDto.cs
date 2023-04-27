using System;
namespace Common.DoorDto
{
	public class TapDoorDto
	{
        public Guid? Id { get; set; }
        public Guid? DoorId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? ActionResultId { get; set; }
        public string DoorName { get; set; }
        public string UserName { get; set; }
        public string TapAction { get; set; }
    }
}

