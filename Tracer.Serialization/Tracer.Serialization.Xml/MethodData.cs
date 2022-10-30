using System.Xml.Schema;
using System.Xml.Serialization;

namespace Tracer.Serialization.Xml
{


//    [XmlRoot( ElementName = "Method" )]
    public class MethodData
    {


        //[XmlAttribute( Form = XmlSchemaForm.Unqualified )]
        public string TimeMs { get; set; } = "";

//        [XmlAttribute( Form = XmlSchemaForm.Unqualified )]
        public string MethodName { get; set; } = "";

  //      [XmlAttribute( Form = XmlSchemaForm.Unqualified )]
        public string ClassName { get; set; } = "";

        public List<MethodData> Methods { get; set; } = new List<MethodData>();

        public MethodData( string methodName, string className, long timeMs, List<MethodData> methods )
        {
            MethodName = methodName;
            ClassName = className;
            TimeMs = $"{timeMs}ms";
            Methods = methods;

        }
        public MethodData()
        {

        }

        public MethodData(Core.MethodData method)
        {
            MethodName = method.MethodName; 
            ClassName = method.ClassName;
            TimeMs = $"{method.TimeMs}ms";
            foreach (var internalMethod in method.Methods)
               Methods.Add( new MethodData( internalMethod ) );


		}



    }
}