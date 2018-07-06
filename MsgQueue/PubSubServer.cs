using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;
using CommonUility;

namespace MsgQueue
{
    /// <summary>
    /// 用一个Proxy实例，同时运行PublishServer和SubcribeServer
    /// 很多模块既要publish数据，也要subcribe其他模块的数据。这个服务器进程相当于中转站
    /// </summary>
    public class PubSubServer
    {
        public static PubSubServer GetInstance()
        {
            return Singleton<PubSubServer>.GetInstance();
        }

        /// <summary>
        /// 初始化PubSubServer。启动一个Task运行
        /// </summary>
        /// <param name="addr">服务地址。可以运行在不同的PC上进行消息的收发</param>
        /// <param name="publiserPort">pub server port。订阅者连接到这个端口</param>
        /// <param name="subscribePort">sub server port。发布者连接到这个端口</param>
        public void InitServer(int publiserPort, int subscribePort, string addr = "127.0.0.1")
        {
            if (HadInited) return;

            _pubSocket = new XPublisherSocket($"@tcp://{addr}:{publiserPort}");
            _subSocket = new XSubscriberSocket($"@tcp://{addr}:{subscribePort}");

            _proxy = new Proxy(_subSocket, _pubSocket);
            Task.Factory.StartNew(_proxy.Start);

            HadInited = true;
        }

        /// <summary>
        /// 停止PubSubServer
        /// </summary>
        public void StopServer()
        {
            if (!HadInited) return;

            _proxy.Stop();
            HadInited = false;
        }

        private Proxy _proxy;
        private NetMQSocket _pubSocket;
        private NetMQSocket _subSocket;

        public bool HadInited { get; private set; }
    }
}
