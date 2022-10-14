using System.Xml.Schema;
using System.Xml.Serialization;

namespace Tracer.Serialization.Xml
{
	[XmlRoot(ElementName = "Root")]
	public class ThreadData
	{




		//core
		[XmlAttribute( Form = XmlSchemaForm.Unqualified )]
		public int Id { get; set; }

		[XmlAttribute( Form = XmlSchemaForm.Unqualified )]
		public long TimeMs { get; set; }

		[XmlAttribute( Form = XmlSchemaForm.Unqualified )]
		public List<MethodData>? Methods { get; }

		public ThreadData(int id, long timeMS, List<MethodData>? methodDatas)
		{
			Id = id;	
			TimeMs = timeMS;
			Methods = methodDatas;
		}

		public ThreadData()
		{

		}

	}
}
