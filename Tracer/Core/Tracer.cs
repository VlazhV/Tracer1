using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
	public class Tracer : ITracer
	{		
		private List<ThreadData> _threadData = new();
		private ConcurrentStack<(int, MethodData, Stopwatch)> _traceStack = new ConcurrentStack<(int, MethodData, Stopwatch)>();
		
		
		
		private Stopwatch _stopWatch;
		private int _currentThreadId;
		private MethodData _currentMethodData;
		private MethodData _prevStackMethodData;
		private bool _isRecursive = false;



		public TraceResult Result()
		{

			TraceResult traceResult = new( _threadData );
			return traceResult;
		}

		
		public void Start()
		{

			_currentThreadId = Thread.CurrentThread.ManagedThreadId;			
			_stopWatch = new Stopwatch();

			_isRecursive = _traceStack.IsEmpty;
			 
			
			_traceStack.Push( ( _currentThreadId, GetFrameInfo(), _stopWatch) );
			_stopWatch.Start();
		}

		public void Stop()
		{

			//_stopWatch.Stop();
			if ( !_traceStack.TryPop( out var result ) )
				throw new NullReferenceException( "Trace Stack is empty" );

			_stopWatch = result.Item3 as Stopwatch;
			_stopWatch.Stop();

			_currentMethodData = result.Item2 as MethodData;
			_currentMethodData.AddInternalMethod( _prevStackMethodData );
			_currentThreadId = result.Item1;

			bool isUsed = false;
			if ( !_traceStack.IsEmpty ) {
				foreach ( var threadData in _threadData )
					if ( threadData.Id == _currentThreadId )
					{
						threadData.AddMethod( _currentMethodData );
						isUsed = true;
					}
				if (!isUsed)				
					_threadData.Add( new ThreadData( _currentThreadId, _currentMethodData ) );
				

				_isRecursive = false;
			}
			else
				_prevStackMethodData = _currentMethodData;
		}


		private MethodData GetFrameInfo()
		{
			StackTrace stackTrace = new StackTrace();
			var frame = stackTrace.GetFrame( 2 );
			string methodName = frame.GetMethod().Name;
			string className = frame.GetMethod().ReflectedType.Name;

			return new MethodData( methodName, className);
		}


		//private (string, string) GetFrameInfo()
		//{
		//	StackTrace stackTrace = new StackTrace();
		//	var frame = stackTrace.GetFrame( 1 );
		//	return (frame.GetMethod().Name, frame.GetMethod().ReflectedType.Name);
		//}

		
	}
}
