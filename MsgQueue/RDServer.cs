
using System.Threading.Tasks;
using CommonUility;
using NetMQ;
using NetMQ.Sockets;

//这个是 router-dealer 模式的服务端
// request1 <--> router <--> dealer <--> response1
// request2 <------^            ^------> response2
//router-dealer模式把同步的req-rep模式变成异步的,但对于使用者来说仍然是同步的
//TODO 未实现完成，不要用
namespace MsgQueue
{
	class RDServer
	{
		private static DealerSocket _dealer;
		private static RouterSocket _router;
		private static Proxy _proxy;

		public static RDServer GetInstance()
		{
			return Singleton<RDServer>.GetInstance();
		}

		public void Init(string addr = "127.0.0.1", int dealerPort = 8877, int routerPort = 8878)
		{
			_router = new RouterSocket($"@tcp://{addr}:{routerPort}");
			_dealer = new DealerSocket($"@tcp://{addr}:{dealerPort}");

			_proxy = new Proxy(_router, _dealer);
			Task.Factory.StartNew(_proxy.Start);
		}

		public void Stop()
		{
			_proxy.Stop();
			_router?.Close();
			_dealer?.Close();
		}
	}
}
