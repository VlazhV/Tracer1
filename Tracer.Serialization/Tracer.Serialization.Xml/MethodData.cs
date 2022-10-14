using System.Xml.Schema;
using System.Xml.Serialization;

namespace Tracer.Serialization.Xml
{


    [XmlRoot( ElementName = "Method" )]
    public class MethodData
    {


        [XmlAttribute( Form = XmlSchemaForm.Unqualified )]
        public string TimeMs { get; set; } = "";

        [XmlElement( ElementName = "Method" )]
        public string MethodName { get; set; } = "";

        [XmlAttribute( Form = XmlSchemaForm.Unqualified )]
        public string ClassName { get; set; } = "";

        public MethodData( string methodName, string className, long timeMs )
        {
            MethodName = methodName;

            ClassName = className;

            TimeMs = $"{timeMs}ms";

        }

        public MethodData()
        {

        }

        public MethodData(Core.MethodData methodData)
        {
            MethodName = methodData.MethodName;
            ClassName = methodData.ClassName;
            TimeMs = $"{methodData.TimeMs}ms";
		}



    }
}