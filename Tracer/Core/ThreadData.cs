using Core;

namespace Core
{
	public class ThreadData
	{
		int _id;
		long _timeMs;
		IReadOnlyList<MethodData> _methods;

		public int Id 
		{ 
			get { return _id; } 			
		}

		public long TimeMs	
		{
			get { return _timeMs; }		
		}

		public IReadOnlyList<MethodData> Methods { get { return _methods; }  }

		

		public ThreadData(int id, List<MethodData> methodData )
		{
			_id = id;
			_methods = methodData;
		}

		public ThreadData( int id, long timeMs, List<MethodData> methodData )
		{
			_id = id;
			_methods = methodData;
			_timeMs = timeMs;
		}
	}
}
