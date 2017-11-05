/*
	ZZZClient.cs

	Copyright 2017 ZZZ Ltd. - Bulgaria

	Licensed under the Apache License, Version 2.0 (the "License");
	you may not use this file except in compliance with the License.
	You may obtain a copy of the License at

	http://www.apache.org/licenses/LICENSE-2.0

	Unless required by applicable law or agreed to in writing, software
	distributed under the License is distributed on an "AS IS" BASIS,
	WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	See the License for the specific language governing permissions and
	limitations under the License.
*/

using System;
using System.Net.Sockets;
using System.Text;

namespace ZZZClientCS
{
    class Program
    {
        static string ZZZProgram(string serverHost, int serverPort, string program)
        {
            try
            {
                if (serverHost.Equals("localhost"))
                    serverHost = "127.0.0.1";
                TcpClient tc = new TcpClient(serverHost, serverPort);
                NetworkStream ns = tc.GetStream();

                if (ns.CanWrite && ns.CanRead)
                {
                    // Do a simple write.
                    byte[] sendBytes = Encoding.UTF8.GetBytes(program + '\0');
                    ns.Write(sendBytes, 0, sendBytes.Length);

                    // Read the NetworkStream into a byte buffer.
                    byte[] bytes = new byte[tc.ReceiveBufferSize];

                    StringBuilder returndata = new StringBuilder();
                    int receivedBytes = ns.Read(bytes, 0, tc.ReceiveBufferSize);
                    returndata.Append(Encoding.UTF8.GetString(bytes, 0, receivedBytes));
                    while (receivedBytes > 0)
                    {
                        receivedBytes = ns.Read(bytes, 0, tc.ReceiveBufferSize);
                        returndata.Append(Encoding.UTF8.GetString(bytes, 0, receivedBytes));
                    }

                    // Return the data received from the host.
                    return returndata.ToString();
                }
                else
                {
                    if (!ns.CanRead)
                    {
                        Console.WriteLine("cannot not read data from this stream");
                        tc.Close();
                    }
                    else
                    {
                        if (!ns.CanWrite)
                        {
                            Console.WriteLine("cannot write data to this stream");
                            tc.Close();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return String.Empty;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            DateTime startTime = DateTime.UtcNow;

            Console.WriteLine(ZZZProgram("localhost", 3333, "#[cout;Hello World from ZZZServer!]"));

            DateTime stopTime = DateTime.UtcNow;
            long elapsedTime = stopTime.Millisecond - startTime.Millisecond;
            Console.WriteLine(elapsedTime.ToString() + " milliseconds");

            Console.ReadKey();
        }
    }
}