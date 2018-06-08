using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsgQueue
{
	public class MqInitial
	{
		public static void Init()
		{
			PubSubServer.GetInstance().InitServer(CommonPort.PubServerPort, CommonPort.SubServerPort);
			SubscribeHelper.GetInstance();
			PublishHelper.GetInstance();
		}

		public static void Stop()
		{
			PublishHelper.GetInstance().Dispose();
			SubscribeHelper.GetInstance().Dispose();
			PubSubServer.GetInstance().StopServer();
		}
	}
}
