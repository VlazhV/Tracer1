using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
	public class MethodInfo
	{

		string _methodName;
		string _className;
		long _timeMs;

		List<MethodInfo> _internalMethods = new();



		public long TimeMs
		{
			get { return _timeMs; }
			set { _timeMs = value; }
		}

		public string MethodName
		{
			get { return _methodName; }
			set { _methodName = value; }
		}

		public string ClassName
		{
			get { return _className; }
			set { _className = value; }
		}

		public List<MethodInfo> Methods { get { return _internalMethods; } set { _internalMethods = value; } }

		public MethodInfo( string methodName, string className )
		{
			_methodName = methodName;
			_className = className;
		}

		private MethodInfo( string methodName, string className, long timeMs, List<MethodInfo> methodDatas )
		{
			_methodName = methodName;
			_className = className;
			_timeMs = timeMs;
			_internalMethods = methodDatas;

		}

		public override bool Equals( object? mi )
		{
			if ( mi == null ) return false;
			var mi1 = (MethodInfo)mi;
			return _methodName == mi1._methodName &&
				   _className == mi1._className;
		}

		

		public MethodInfo Clone()
		{
			return new MethodInfo( _methodName, _className, _timeMs, _internalMethods );
		}


		public static MethodData ToMethodData( MethodInfo mi )
		{
			var methodDatas = new List<MethodData>();
			if (mi._internalMethods != null)
				foreach(var mi1 in mi._internalMethods)
				{
					methodDatas.Add( ToMethodData(mi1));
				}
			MethodData md = new MethodData( mi._methodName, mi._className, mi._timeMs, methodDatas );
			return md;
		}

	}
}
