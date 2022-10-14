using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
	public class TraceResult
	{
		IReadOnlyList<ThreadData> _traceInfo;

		public IReadOnlyList<ThreadData> TraceInfo { get { return _traceInfo; } set { _traceInfo = value; } }

		public TraceResult( IReadOnlyList<ThreadData> traceInfo )
		{
			_traceInfo = traceInfo;
		}
	}
}
