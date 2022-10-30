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
		private List<ThreadInfo> _threadInfo = new();
		private ConcurrentDictionary<int, Stack<(MethodInfo, Stopwatch)>> _traceStacks = new();

		public ConcurrentDictionary<int, Stack<(MethodInfo, Stopwatch)>> TraceStacks 
		{
			get { return _traceStacks; }
		}


		public TraceResult Result()
		{
			List<ThreadData> threadData = new List<ThreadData>();

			foreach (var threadInfo in _threadInfo)
			{
				long threadTimeMs = 0;
				foreach (var method in threadInfo.Methods)				
					threadTimeMs += method.TimeMs;
				threadInfo.TimeMs = threadTimeMs;

				threadData.Add(threadInfo.ToThreadData());
			}

			


			TraceResult traceResult = new( threadData );
			return traceResult;
		}

		
		public void Start()
		{

			int currentThreadId = Thread.CurrentThread.ManagedThreadId;

			Stack<(MethodInfo, Stopwatch)> stack = AddOrGet( currentThreadId );
			Stopwatch stopWatch = new Stopwatch();


			
			stack.Push( ( GetFrameInfo(), stopWatch) );
			
			
			stopWatch.Start();
		}

		public void Stop()
		{
			
			int currentThreadId = Thread.CurrentThread.ManagedThreadId;
			Stack<(MethodInfo, Stopwatch)> stack = _traceStacks[ currentThreadId ];
			( MethodInfo, Stopwatch ) result;
			if ( !stack.TryPop( out result ) ) 
				throw new NullReferenceException( "Trace Stack is empty" );


			Stopwatch stopWatch = result.Item2 as Stopwatch;
			stopWatch.Stop();

			MethodInfo currentMethodInfo = result.Item1 as MethodInfo;
			currentMethodInfo.TimeMs = stopWatch.ElapsedMilliseconds;
			

			bool isUsed = false;
			if ( stack.Count == 0 ) {
				foreach ( var threadInfo in _threadInfo )
					if ( threadInfo.Id == currentThreadId )
					{
						threadInfo.Methods.Add( currentMethodInfo );
						isUsed = true;
						break;
					}
				if (!isUsed)				
					_threadInfo.Add( new ThreadInfo( currentThreadId, currentMethodInfo ) );

			}
			else
			{
				
				if (stack.TryPeek (out var prev))
					prev.Item1.Methods.Add( currentMethodInfo );
			}
				
		}

		private Stack<(MethodInfo, Stopwatch)> AddOrGet(int tid)
		{
			if ( _traceStacks.ContainsKey( tid ) )
			{ 
				return _traceStacks[tid];
			}
			else
			{
				var newStack = new Stack<(MethodInfo, Stopwatch)>();
				while ( !_traceStacks.TryAdd( tid, newStack ) ) ;
				return newStack;
			}
		}

		private MethodInfo GetFrameInfo()
		{
			StackTrace stackTrace = new StackTrace();
			var frame = stackTrace.GetFrame( 2 );			
				
			string methodName = frame.GetMethod().Name;
			string className = frame.GetMethod().ReflectedType.Name;

			return new MethodInfo( methodName, className);
		}



		
	}
}
