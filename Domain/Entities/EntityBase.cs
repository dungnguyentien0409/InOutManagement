﻿using System;
namespace Entities
{
	public abstract class EntityBase
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
	}
}

