using System.Xml.Schema;
using System.Xml.Serialization;

namespace Tracer.Serialization.Xml
{
	//[XmlRoot(ElementName = "Thread")]
	public class ThreadData
	{

		//	[XmlAttribute( Form = XmlSchemaForm.Unqualified )]
		public string Id { get; set; } = "";

		//		[XmlAttribute( Form = XmlSchemaForm.Unqualified )]
		public string TimeMs { get; set; } = "";

		//[XmlElement(ElementName = "Method" )]
		public List<MethodData> Methods { get; set; } = new List<MethodData>();

		public ThreadData(int id, long timeMS, List<MethodData> methods)
		{
			Id = id.ToString();	
			TimeMs = $"{timeMS}ms";
			Methods = methods;
		}

		public ThreadData()
		{

		}

		public ThreadData(Core.ThreadData threadData)
		{
			Id = threadData.Id.ToString();
			TimeMs = $"{threadData.TimeMs}ms";
			foreach ( var methodData in threadData.Methods )
				Methods.Add( new MethodData(methodData) );

		}
	}
}
