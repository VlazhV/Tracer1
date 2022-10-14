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

        public void InnerMethod()
        {
            _tracer.Start();            
            Thread.Sleep( 100 );
            _tracer.Stop();
        }
    }
}
