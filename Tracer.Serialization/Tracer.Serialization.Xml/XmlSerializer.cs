using Serialization.Abstraction;

namespace Tracer.Serialization.Xml
{
	public class XmlSerializer : ITraceResultSerializer
	{
		private string format = "xml";
		public string Format { get { return format; } }

		public void Serialize( Core.TraceResult traceResult, Stream to )
		{			
			System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer( typeof( TraceResult ) );
			xmlSerializer.Serialize(to, new TraceResult(traceResult));

		}

	}
}