using Core;
using Serialization.Abstraction;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;


namespace Tracer.Serialization.Yaml
{
	public class YamlSerializer : ITraceResultSerializer
	{
		private string format = "yaml";
		public string Format { get { return format; } }

		public void Serialize( TraceResult traceResult, Stream to )
		{
			var serializer = new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
			using TextWriter textWriter = new StreamWriter( to );
			serializer.Serialize( textWriter, traceResult );

		}
	}
}