using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRPCStream.Protocol;
using Grpc.Core;
using Google.Protobuf;

namespace gRPCStream.Server
{
    public class RpcStreamServiceImpl : RpcStreamService.RpcStreamServiceBase
    {
        public async override Task GetStreamContent(IAsyncStreamReader<StreamRequest> requestStream, IServerStreamWriter<StreamResponse> responseStream, ServerCallContext context)
        {
            while(true)
            {
                //bool ret = await requestStream.MoveNext();
                //if (ret)
                //{
                //    Console.WriteLine("receive request:" + requestStream.Current.Request.ToStringUtf8());
                //}

                //byte[] bytes = System.Text.Encoding.Default.GetBytes("server response");
                await Task.Run(async () =>
                {
                    for(int i=0; i <= 20; i++)
                    {
                        await Task.Delay(1000);
                        await responseStream.WriteAsync(new StreamResponse { Response = ByteString.CopyFromUtf8("server response "+i.ToString()) });
                    }
                    
                });
            }
        }
    }
}
