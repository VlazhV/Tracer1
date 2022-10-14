using System.Xml.Schema;
using System.Xml.Serialization;

namespace Tracer.Serialization.Xml
{
	[XmlRoot(ElementName = "Thread")]
	public class ThreadData
	{




		//core
		[XmlAttribute( Form = XmlSchemaForm.Unqualified )]
		public int Id { get; set; }

		[XmlAttribute( Form = XmlSchemaForm.Unqualified )]
		public long TimeMs { get; set; }

		[XmlElement(ElementName = "Method" )]
		public List<MethodData> Methods { get; }

		public ThreadData(int id, long timeMS, List<MethodData> methodDatas)
		{
			Id = id;	
			TimeMs = timeMS;
			Methods = methodDatas;
		}

		public ThreadData()
		{

		}

		public ThreadData(Core.ThreadData threadData)
		{
			Id = threadData.Id;
			TimeMs = threadData.TimeMs;
			foreach ( var methodData in threadData.Methods )
				Methods.Add( new MethodData(methodData) );

		}
	}
}
