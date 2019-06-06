using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using GRPCStream.Protocol;

namespace gRPCStream.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Grpc.Core.Server server = new Grpc.Core.Server
            {
                Services = { RpcStreamService.BindService(new RpcStreamServiceImpl()) },
                Ports = { new ServerPort("127.0.0.1", 40001, ServerCredentials.Insecure) }
            };
            server.Start();
            Console.WriteLine("grpc server started,listening on port 40001");
            Console.ReadKey();
        }
    }
}
