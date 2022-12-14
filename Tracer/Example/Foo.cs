using Core;

namespace Example
{
	public class Foo
	{
        private ITracer _tracer;
        private Bar bar;

        public Foo()
        {
        }

        public Foo( ITracer tracer )
        {
            _tracer = tracer;
            bar = new Bar( tracer );
        }

        public void DoSomething()
        {
            _tracer.Start();
            bar.InnerMethod1();
            Thread.Sleep( 2000 );
            bar.InnerMethod2();
            bar.InnerMethod3();
            _tracer.Stop();
        }
    }
}
