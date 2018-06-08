using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUility;

/// <summary>
/// warnning: 可能会有性能限制，毕竟是在一个线程中。后面再加上PULL和PUSH模式
/// </summary>
namespace MsgQueue
{
	public class SubscribeHelper : IDisposable
	{
		private SubscribeClient subClient;

		public SubscribeHelper()
		{
			subClient = new SubscribeClient(CommonPort.PubServerPort);
			subClient.Run();
		}

		~SubscribeHelper()
		{
			subClient.Dispose();
		}

		public void Dispose()
		{
			
		}

		public static SubscribeHelper GetInstance()
		{
			return Singleton<SubscribeHelper>.GetInstance();
		}

		public static bool AddSubscribe(string topic, HandlerSubscribeMsg handler)
		{
			return GetInstance().SubscribeTopic(topic, handler);
		}

		public static bool CancelSubscribe(string topic)
		{
			return GetInstance().SubScribeCancel(topic);
		}

		//TODO 需要加topic流程
		private bool SubscribeTopic(string topic, HandlerSubscribeMsg handler)
		{
			subClient.AddSubscribeTopic(topic, handler);
			return true;
		}

		private bool SubScribeCancel(string topic)
		{
			subClient.CancelSubscribeTopic(topic);
			return true;
		}

	}
}
