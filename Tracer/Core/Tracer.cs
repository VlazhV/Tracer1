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
		
		
		private MethodData _currentMethodData;
		private MethodData _prevStackMethodData;




		public TraceResult Result()
		{
			
			foreach (var threadData in _threadData)
			{
				long threadTimeMs = 0;
				foreach (var method in threadData.Methods)				
					threadTimeMs += method.TimeMs;
				threadData.TimeMs = threadTimeMs;

			}
			TraceResult traceResult = new( _threadData );
			return traceResult;
		}

		
		public void Start()
		{
			
			int currentThreadId = Thread.CurrentThread.ManagedThreadId;			
			Stopwatch stopWatch = new Stopwatch();


			_currentMethodData = GetFrameInfo();
			_traceStack.Push( ( currentThreadId, _currentMethodData, stopWatch) );
			
			
			stopWatch.Start();
		}

		public void Stop()
		{
		
			//_stopWatch.Stop();
			if ( !_traceStack.TryPop( out var result ) )
				throw new NullReferenceException( "Trace Stack is empty" );

			Stopwatch stopWatch = result.Item3 as Stopwatch;
			stopWatch.Stop();

			_currentMethodData = result.Item2 as MethodData;

//			_currentMethodData.AddInternalMethod( _prevStackMethodData );

			_currentMethodData.TimeMs = stopWatch.ElapsedMilliseconds;
			int currentThreadId = result.Item1;

			bool isUsed = false;
			if ( _traceStack.IsEmpty ) {
				foreach ( var threadData in _threadData )
					if ( threadData.Id == currentThreadId )
					{
						threadData.AddMethod( _currentMethodData );
						isUsed = true;
					}
				if (!isUsed)				
					_threadData.Add( new ThreadData( currentThreadId, _currentMethodData ) );

			}
			else
			{
				if ( _traceStack.TryPop( out var prev ))
				{
					prev.Item2.AddInternalMethod( _currentMethodData );
					_traceStack.Push( prev );
				}
				
			}
				
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
