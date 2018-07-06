using System;
using NetMQ;
using NetMQ.Sockets;

namespace MsgQueue
{
    /// <summary>
    /// publish客户端。不想用全局的实例，或者要发布到其他端口，使用这个类
    /// </summary>
    public class PublisherClient : IDisposable
    {
        private readonly PublisherSocket _pubSocket;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="port">publish消息到哪个端口对应的队列</param>
        /// <param name="addr">中转服务器所在的PC地址</param>
        public PublisherClient(int port, string addr = "127.0.0.1")
        {
            _pubSocket = new PublisherSocket($">tcp://{addr}:{port}");
            _pubSocket.Options.SendHighWatermark = 1000;
        }

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="topic">发送消息对应的主题</param>
        /// <param name="msg">消息内容</param>
        public void PublishMsg(string topic, string msg)
        {
            _pubSocket.SendMoreFrame(topic).SendFrame(msg);
        }

        /// <summary>
        /// 发布消息。重载函数
        /// </summary>
        /// <param name="topic">发送消息对应的主题</param>
        /// <param name="msg">消息内容</param>
        public void PublishMsg(string topic, byte[] msg)
        {
            _pubSocket.SendMoreFrame(topic).SendFrame(msg, msg.Length);
        }

        private void PublishMsg(string msg)
        {
            _pubSocket.SendFrame(msg);
        }

        public void PublishMsg(byte[] msg)
        {
            _pubSocket.SendFrame(msg);
        }

        public void Dispose()
        {
            _pubSocket?.Close();
        }
    }
}
