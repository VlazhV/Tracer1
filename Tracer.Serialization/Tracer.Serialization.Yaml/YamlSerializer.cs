using Core;
using Serialization.Abstraction;

namespace Tracer.Serialization.Yaml
{
	public class YamlSerializer : ITraceResultSerializer
	{
		private string format = "yaml";
		public string Format { get { return format; } }

		public void Serialize( TraceResult traceResult, Stream to )
		{
			throw new NotImplementedException();
		}
	}
}