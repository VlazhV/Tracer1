using Core;

namespace Example
{
	public class Bar
	{
        private ITracer _tracer;

        public Bar( ITracer tracer )
        {
            _tracer = tracer;
        }

        public void InnerMethod1()
        {
            _tracer.Start();            
            Thread.Sleep( 150 );
            _tracer.Stop();
        }

        public void InnerMethod2()
        {
            _tracer.Start();
            Thread.Sleep( 150 );
            _tracer.Stop();
        }

        public void InnerMethod3()
        {
            _tracer.Start();
            Thread.Sleep( 150 );
            _tracer.Stop();
        }
    }
}
