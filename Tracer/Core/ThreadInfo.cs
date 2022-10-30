using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
	public class ThreadInfo
	{
		int _id;
		long _timeMs;
		List<MethodInfo> _methods = new();

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

		public List<MethodInfo> Methods { get { return _methods; } set { _methods = value; } }



		public ThreadInfo( int id, MethodInfo methodInfo )
		{
			_id = id;
			_methods.Add( methodInfo );
		}

		public ThreadData ToThreadData()
		{
			var methodDatas = new List<MethodData>();
			foreach ( MethodInfo methodInfo in _methods )
			{
				methodDatas.Add( MethodInfo.ToMethodData( methodInfo ) );
			}
			return new ThreadData( _id, _timeMs,  methodDatas );
		}




	}
}
