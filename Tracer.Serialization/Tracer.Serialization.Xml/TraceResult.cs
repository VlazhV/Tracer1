using System.Text;
using System.Xml.Serialization;
using Tracer.Serialization.Xml;


namespace Tracer.Serialization.Xml;

[XmlRoot( ElementName = "Root" )]
public class TraceResult
{

    public TraceResult()
    { 
    }

    // Core.TraceResult traceResult
    List<ThreadData> _traceInfo = new();

    public List<ThreadData> TraceInfo { get { return _traceInfo; } set { _traceInfo = value; } }

    public TraceResult( Core.TraceResult traceResult )
    {
        foreach( Core.ThreadData threadData in traceResult.TraceInfo )
        {
			_traceInfo.Add( new ThreadData( threadData ) );
		}

    }

}