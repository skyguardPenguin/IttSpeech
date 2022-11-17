using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace AudioListener.Syntactic
{

    public class SyntacticAnalizer
    {

        public static async Task<string> AnalizeEntry(string path, int port, string entry)
        {
            IPHostEntry ipHostInfo;
            IPAddress ipAddress;
            IPEndPoint ipEndPoint;

            int timeOut = 5; //Max waiting time in seconds
           
            
            ipHostInfo = await Dns.GetHostEntryAsync(path);
            ipAddress = ipHostInfo.AddressList[0];
            ipEndPoint = new(ipAddress, port);
            using Socket client = new(
                    ipEndPoint.AddressFamily,
                    SocketType.Stream,
                    ProtocolType.Tcp);

           

           await client.ConnectAsync(ipEndPoint);
            var message = entry;
            var messageBytes = Encoding.UTF8.GetBytes(message);
            _ = await client.SendAsync(messageBytes, SocketFlags.None);
            Console.WriteLine($"Socket client sent message: \"{message}\"");
            DateTime time = DateTime.Now.AddSeconds(timeOut);
            while (true)
            {
                // Send message.


                // Receive ack.
                var buffer = new byte[1_024];
                var received = await client.ReceiveAsync(buffer, SocketFlags.None);
                var response = Encoding.UTF8.GetString(buffer, 0, received);
                if (response != "<|ACK|>")
                {
                    Console.WriteLine(
                        $"Socket client received acknowledgment: \"{response}\"");
                    client.Shutdown(SocketShutdown.Both);
                    return response;
                   
                }
                else if(time < DateTime.Now)
                {
                    break;
                }
                // Sample output:
                //     Socket client sent message: "Hi friends 👋!<|EOM|>"
                //     Socket client received acknowledgment: "<|ACK|>"
            }
            client.Shutdown(SocketShutdown.Both);
            return String.Empty;


        }
    }
}
