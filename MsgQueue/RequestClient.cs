using NetMQ.Sockets;

//request client连接到router
// TODO 未实现
namespace MsgQueue
{
	class RequestClient
	{
		private RequestSocket _requestSocket;

		public int RouterPort { get; private set; }

		public string Id { get; set; }

		public string RouterAddr { get; private set; }


		//id是为了区分不同的
		public RequestClient(int port, string id, string addr = "127.0.0.1" )
		{
			RouterPort = port;
			Id = id;
			RouterAddr = addr;
		}

		public bool Init()
		{
			//判断ID是否重复

			_requestSocket = new RequestSocket($">tcp://{RouterAddr}:{RouterPort}");
			return true;
		}

		public void SendReq()
		{
			
		}
	}
}
