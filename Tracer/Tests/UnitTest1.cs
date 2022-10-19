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
			
		}




		[TestMethod]
		public void TraceResultComplete()
		{
			Tracer tracer = new Tracer();
			Foo foo = new Foo( tracer );
			Bar bar = new Bar( tracer );

			

			Thread thread = new Thread( bar.InnerMethod3 );			
			thread.Start();
			bar.InnerMethod1();
			bar.InnerMethod2();
			foo.DoSomething();
			thread.Join();



			TraceResult traceResult = tracer.Result();
			traceResult = tracer.Result();
			Assert.IsNotNull( traceResult );			
			Assert.AreEqual( 2, traceResult.TraceInfo.Count);
			Assert.IsTrue  ( traceResult.TraceInfo[ 1 ].TimeMs >= 2800);
			Assert.AreEqual( "Foo", traceResult.TraceInfo[ 1 ].Methods[ 2 ].ClassName );
			Assert.AreEqual( "DoSomething", traceResult.TraceInfo[ 1 ].Methods[ 2 ].MethodName );
			Assert.AreEqual( 3, traceResult.TraceInfo[ 1 ].Methods[ 2 ].Methods.Count );
			
			Assert.AreEqual( "InnerMethod1", traceResult.TraceInfo[ 1 ].Methods[ 2 ].Methods[ 0 ].MethodName );
			Assert.AreEqual( "Bar", traceResult.TraceInfo[ 1 ].Methods[ 2 ].Methods[ 0 ].ClassName );
			Assert.IsTrue( traceResult.TraceInfo[ 1 ].Methods[ 2 ].Methods[ 0 ].TimeMs >= 200 );

			Assert.AreEqual( "InnerMethod2", traceResult.TraceInfo[ 1 ].Methods[ 2 ].Methods[ 1 ].MethodName );
			Assert.AreEqual( "Bar", traceResult.TraceInfo[ 1 ].Methods[ 2 ].Methods[ 1 ].ClassName );
			Assert.IsTrue( traceResult.TraceInfo[ 1 ].Methods[ 2 ].Methods[ 1 ].TimeMs >= 150 );

			Assert.AreEqual( "InnerMethod3", traceResult.TraceInfo[ 1 ].Methods[ 2 ].Methods[ 2 ].MethodName );
			Assert.AreEqual( "Bar", traceResult.TraceInfo[ 1 ].Methods[ 2 ].Methods[ 2 ].ClassName );
			Assert.IsTrue  ( traceResult.TraceInfo[ 1 ].Methods[ 2 ].Methods[ 2 ].TimeMs >= 100 );

			Assert.AreEqual(0,  traceResult.TraceInfo[ 1 ].Methods[ 0 ].Methods.Count );
			Assert.AreEqual(0,  traceResult.TraceInfo[ 1 ].Methods[ 1 ].Methods.Count );
		}
	}
}
