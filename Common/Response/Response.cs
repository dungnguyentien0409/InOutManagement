using System;
namespace Response
{
	public class Response<T> where T : class
	{
		public string Message;
		public T Result;
	}
}

