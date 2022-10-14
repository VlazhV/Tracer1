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
    IReadOnlyList<ThreadData> _traceInfo;

    public IReadOnlyList<ThreadData> TraceInfo { get { return _traceInfo; } set { _traceInfo = value; } }

    public TraceResult( Core.TraceResult traceResult )
    {
        //_traceInfo = traceResult.TraceInfo;
    }

}