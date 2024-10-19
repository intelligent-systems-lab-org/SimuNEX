using System;
using System.Net;
using System.Net.Sockets;

namespace SimuNEX.Communication
{
    public class UDP : COMProtocol, IProtocolInitialization
    {
        public string IP_Address = "127.0.0.1";

        private UdpClient udpClient;

        [COMType(Streaming.S)]
        public int sendPort = 5555;

        [COMType(Streaming.R)]
        public int receivePort = 5556;

        [COMType(Streaming.S)]
        private IPEndPoint sendEndPoint;

        [COMType(Streaming.R)]
        private IPEndPoint receiveEndPoint;

        private byte[] _sendBuffer;

        public void Initialize()
        {
            sendEndPoint = new(IPAddress.Parse(IP_Address), sendPort);
            receiveEndPoint = new(IPAddress.Parse(IP_Address), receivePort);

            udpClient = new();

            _sendBuffer = new byte[sendDataSize * sizeof(float)];
        }

        public override void Receive(float[] data)
        {
            _ = udpClient.BeginReceive(ReceiveData, data);
        }

        public override void Send(in float[] data)
        {
            Buffer.BlockCopy(data, 0, _sendBuffer, 0, _sendBuffer.Length);
            _ = udpClient.Send(_sendBuffer, _sendBuffer.Length, sendEndPoint);
        }

        private void ReceiveData(IAsyncResult result)
        {
            byte[] receivedBytes = udpClient.EndReceive(result, ref receiveEndPoint);

            float[] targetData = (float[])result.AsyncState;
            Buffer.BlockCopy(receivedBytes, 0, targetData, 0, receivedBytes.Length);
        }
    }
}
