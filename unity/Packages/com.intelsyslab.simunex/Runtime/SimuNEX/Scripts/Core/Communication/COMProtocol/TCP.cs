using System;
using System.Net;
using System.Net.Sockets;

namespace SimuNEX.Communication
{
    public class TCP : COMProtocol, IProtocolInitialization
    {
        public string IP_Address = "127.0.0.1";
        private TcpClient sendClient;
        private TcpClient receiveClient;

        [COMType(Streaming.S)]
        public int sendPort = 5555;

        [COMType(Streaming.R)]
        public int receivePort = 5556;

        [COMType(Streaming.S)]
        private NetworkStream sendStream;

        [COMType(Streaming.R)]
        private NetworkStream receiveStream;

        private byte[] _sendBuffer;
        private byte[] _receiveBuffer;

        public void Initialize()
        {
            sendClient = new();
            sendClient.Connect(IPAddress.Parse(IP_Address), sendPort);
            sendStream = sendClient.GetStream();

            receiveClient = new();
            receiveClient.Connect(IPAddress.Parse(IP_Address), receivePort);
            receiveStream = receiveClient.GetStream();

            _sendBuffer = new byte[sendDataSize * sizeof(float)];
            _receiveBuffer = new byte[receiveDataSize * sizeof(float)];
        }

        public override void Receive(float[] data)
        {
            _ = receiveStream.BeginRead
            (
                _receiveBuffer,
                0,
                receiveClient.ReceiveBufferSize,
                ReceiveData,
                data
            );
        }

        public override void Send(in float[] data)
        {
            Buffer.BlockCopy(data, 0, _sendBuffer, 0, _sendBuffer.Length);

            sendStream.Write(_sendBuffer, 0, _sendBuffer.Length);
            sendStream.Flush();
        }

        private void ReceiveData(IAsyncResult result)
        {
            int bytesRead = receiveStream.EndRead(result);

            float[] targetData = (float[])result.AsyncState;
            Buffer.BlockCopy(_receiveBuffer, 0, targetData, 0, bytesRead);
        }
    }
}
