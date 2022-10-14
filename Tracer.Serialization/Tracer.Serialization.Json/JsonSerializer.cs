using Core;
using Serialization.Abstraction;
using System.Text;


namespace Tracer.Serialization.Json
{
	public class JsonSerializer : ITraceResultSerializer
	{
		private string format = "json";
		public string Format { get { return format; } }

		public void Serialize( TraceResult traceResult, Stream to )
		{
			var options = new System.Text.Json.JsonSerializerOptions
			{ WriteIndented = true };
			string json = System.Text.Json.JsonSerializer.Serialize( traceResult, options );
			to.Write( Encoding.UTF8.GetBytes( json ), 0, json.Length );
			

		}
	}
}