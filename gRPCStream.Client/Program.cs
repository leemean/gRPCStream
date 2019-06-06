using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;
using Grpc.Core;
using GRPCStream.Protocol;

namespace gRPCStream.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Channel channel = new Channel("127.0.0.1:40001",ChannelCredentials.Insecure);
            RpcStreamService.RpcStreamServiceClient client = new RpcStreamService.RpcStreamServiceClient(channel);

            var resultContent = client.GetStreamContent();
            await resultContent.RequestStream.WriteAsync(new StreamRequest { Request = ByteString.CopyFromUtf8("client first request")}); 
            IAsyncStreamReader<StreamResponse> iter = resultContent.ResponseStream;
            while (true)
            {
                await Task.Delay(1000);
                bool ifnext = await iter.MoveNext();
                MemoryStream stream = new MemoryStream();

                //StreamReader reader = new StreamReader(stream);
                //string text = reader.ReadToEnd();
                if (ifnext)
                {
                    Console.WriteLine(iter.Current.Response.ToStringUtf8());
                }
            }
        }
    }
}
