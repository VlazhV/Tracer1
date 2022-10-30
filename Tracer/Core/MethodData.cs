using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
	public class MethodData
	{
		string _methodName;
		string _className;
		long _timeMs;

		IReadOnlyList<MethodData> _internalMethods;
		


		public long TimeMs 
		{ 
			get { return _timeMs; } 			
		}

		public string MethodName
		{
			get { return _methodName; }			
		}

		public string ClassName
		{
			get { return _className; }	
		}

		public IReadOnlyList<MethodData> Methods { get { return _internalMethods; } }

		public MethodData(string methodName, string className){
			_methodName = methodName;
			_className = className;			
		}

		public MethodData( string methodName, string className, long timeMs, List<MethodData> methodDatas )
		{
			_methodName = methodName;
			_className = className;
			_timeMs = timeMs;
			_internalMethods = methodDatas;
		}

		private MethodData( string methodName, string className, long timeMs, IReadOnlyList<MethodData> methodDatas )
		{
			_methodName = methodName;
			_className = className;
			_timeMs = timeMs;
			_internalMethods = methodDatas;
		}

		public override bool Equals( object? md )
		{
			if ( md == null ) return false;
			var md1 = (MethodData)md;
			return _methodName == md1._methodName &&
				   _className == md1._className;				   
		}
	

		public MethodData Clone()
		{
			return new MethodData( _methodName, _className, _timeMs, _internalMethods );
		}
		

	}
}
