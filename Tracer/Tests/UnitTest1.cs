using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core;
using Example;
using System.Threading;

namespace Tests
{

	[TestClass]
	public class UnitTest1
	{
		private Tracer _tracer;
		private TraceResult _traceResult;
		[TestInitialize]
		public void Initialization()
		{
			// _traceResult = new TraceResult();
			// _tracer = new Tracer();
			// ThreadsHolder.threads = _traceResult;
		}




		[TestMethod]
		public void TraceResultComplete()
		{
			Tracer tracer = new Tracer();
			Foo foo = new Foo( tracer );
			Thread thread;

			thread = new Thread( foo.DoSomething );
			// new ThreadInfo( tracer, thread, 1 );
			thread.Start();

			foreach ( var keyValuePair in tracer.ThreadInfoDictionary )
			{
				keyValuePair.Key.Join();
			}

			TraceResult traceResult = tracer.Result();
			traceResult = tracer.Result();
			Assert.IsNotNull( traceResult );
			Assert.AreEqual( traceResult.Threads[ 0 ].Id, "1" );
			Assert.AreEqual( traceResult.Threads.Count, 1 );
			Assert.IsTrue( traceResult.Threads[ 0 ].TimeInt >= 2900 );
			Assert.AreEqual( traceResult.Threads[ 0 ].Methods[ 0 ].ClassName, "Foo" );
			Assert.AreEqual( traceResult.Threads[ 0 ].Methods[ 0 ].Name, "DoSomething" );
			Assert.AreEqual( traceResult.Threads[ 0 ].Methods[ 0 ].Methods.Count, 2 );
			Assert.AreEqual( traceResult.Threads[ 0 ].Methods[ 0 ].Methods[ 0 ].Name, "InnerMethod" );
			Assert.AreEqual( traceResult.Threads[ 0 ].Methods[ 0 ].Methods[ 0 ].ClassName, "Bar" );
			Assert.IsTrue( traceResult.Threads[ 0 ].Methods[ 0 ].Methods[ 0 ].Time.StartsWith( "2" ) );
			Assert.IsTrue( traceResult.Threads[ 0 ].Methods[ 0 ].Methods[ 1 ].Time.StartsWith( "7" ) );
			Assert.IsTrue( traceResult.Threads[ 0 ].Methods[ 0 ].Methods[ 0 ].Methods.Count == 0 );
			Assert.IsTrue( traceResult.Threads[ 0 ].Methods[ 0 ].Methods[ 1 ].Methods.Count == 0 );
		}
	}
}
}