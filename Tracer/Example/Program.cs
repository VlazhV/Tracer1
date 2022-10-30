using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Core;
using Serialization.Abstraction;
using static System.Threading.Thread;


namespace Example
{
	

	public static class Program
	{
        static void Main( string[] args )
        {
            Tracer tracer = new Tracer();
            Foo foo = new Foo( tracer );
            Bar bar = new Bar( tracer );
            Thread thread;
            
            

            thread = new Thread( foo.DoSomething );
            thread.Start();
            bar.InnerMethod1();
            bar.InnerMethod2();
            foo.DoSomething();
            thread.Join();
            
            
            

            TraceResult traceResult = tracer.Result();
            var files = Directory.EnumerateFiles( "TraceResultSerializers", "*.dll" );
            foreach ( var file in files )
            {
                var serializerAssembly = Assembly.LoadFrom( file );
                var types = serializerAssembly.GetTypes();
                foreach ( var type in types )
                {
                    if ( type.GetInterface( nameof( ITraceResultSerializer ) ) == null )
                        continue;
                    var serializer = (ITraceResultSerializer?)Activator.CreateInstance( type );
                    if ( serializer == null )
                    {
                        throw new Exception( $"Serializer {type} not created" );
                    }
                    using var fileStream = new FileStream( $"results/result.{serializer.Format}", FileMode.Create );
                    serializer.Serialize( traceResult, fileStream);
                }
            }
        }
    }
}