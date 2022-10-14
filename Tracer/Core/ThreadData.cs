using Core;

namespace Core
{
	public class ThreadData
	{
		int _id;
		long _timeMs;
		List<MethodData>? _methods = null;

		public int Id 
		{ 
			get { return _id; } 
			set { _id = value; }
		}

		public long TimeMs	
		{
			get { return _timeMs; }
			set { _timeMs = value; }
		}

		public List<MethodData>? Methods { get { return _methods; } }

		public void AddMethod(MethodData methodData)
		{
			if (_methods == null)
				_methods = new List<MethodData>();
			_methods.Add(methodData);
		}

		public ThreadData(int id, MethodData methodData )
		{
			_id = id;
			AddMethod( methodData );
		}

	}
}
