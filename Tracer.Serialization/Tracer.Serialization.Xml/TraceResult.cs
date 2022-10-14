using System.Text;
using System.Xml.Serialization;
using Tracer.Serialization.Xml;
using Core;

namespace Tracer.Serialization.Xml;

[XmlRoot( ElementName = "Root" )]
public class TraceResult
{

    public TraceResult()
    { 
    }

    // Core.TraceResult traceResult
    List<ThreadData> _traceInfo;

    public List<ThreadData> TraceInfo { get { return _traceInfo; } set { _traceInfo = value; } }

    public TraceResult( Core.TraceResult traceResult )
    {
        foreach( Core.ThreadData threadData in traceResult.TraceInfo )
            TraceInfo.Add( new ThreadData(threadData));
    }

}