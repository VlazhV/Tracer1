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
		private ConcurrentDictionary<int, ConcurrentStack<(MethodData, Stopwatch)>> _traceStacks = new();

		public ConcurrentDictionary<int, ConcurrentStack<(MethodData, Stopwatch)>> TraceStacks 
		{
			get { return _traceStacks; }
		}


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

			ConcurrentStack<(MethodData, Stopwatch)> stack = AddOrGet( currentThreadId );
			Stopwatch stopWatch = new Stopwatch();


			
			stack.Push( ( GetFrameInfo(), stopWatch) );
			
			
			stopWatch.Start();
		}

		public void Stop()
		{
			
			int currentThreadId = Thread.CurrentThread.ManagedThreadId;
			ConcurrentStack<(MethodData, Stopwatch)> stack = AddOrGet( currentThreadId );
			( MethodData, Stopwatch ) result;
			if ( !stack.TryPop( out result ) ) 
				throw new NullReferenceException( "Trace Stack is empty" );


			Stopwatch stopWatch = result.Item2 as Stopwatch;
			stopWatch.Stop();

			MethodData currentMethodData = result.Item1 as MethodData;
			currentMethodData.TimeMs = stopWatch.ElapsedMilliseconds;
			

			bool isUsed = false;
			if ( stack.IsEmpty ) {
				foreach ( var threadData in _threadData )
					if ( threadData.Id == currentThreadId )
					{
						threadData.AddMethod( currentMethodData );
						isUsed = true;
					}
				if (!isUsed)				
					_threadData.Add( new ThreadData( currentThreadId, currentMethodData ) );

			}
			else
			{
				
				if (stack.TryPeek (out var prev))
					prev.Item1.AddInternalMethod( currentMethodData );
			}
				
		}

		private ConcurrentStack<(MethodData, Stopwatch)> AddOrGet(int tid)
		{
			if ( _traceStacks.ContainsKey( tid ) )
			{ 
				return _traceStacks[tid];
			}
			else
			{
				var newStack = new ConcurrentStack<(MethodData, Stopwatch)>();
				while ( !_traceStacks.TryAdd( tid, newStack ) ) ;
				return newStack;
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



		
	}
}
