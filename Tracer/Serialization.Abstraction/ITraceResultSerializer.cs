using Core;

namespace Serialization.Abstraction
{
	public interface ITraceResultSerializer
	{
		string Format { get; }
		void Serialize( TraceResult traceResult, Stream to );
	}
}
