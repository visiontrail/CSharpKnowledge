using System;
using CommonUility;

namespace MsgQueue
{
	/// <summary>
	/// 公共的发布消息助手
	/// </summary>
	public class PublishHelper : IDisposable
	{
		private readonly PublisherClient _pubClient;

		public PublishHelper()
		{
			_pubClient = new PublisherClient(CommonPort.SubServerPort);
		}

		~PublishHelper()
		{
			_pubClient.Dispose();
		}

		public void Dispose()
		{
			
		}

		public static PublishHelper GetInstance()
		{
			return Singleton<PublishHelper>.GetInstance();
		}

		public void Publish(string topic, string msg)
		{
			_pubClient.PublishMsg(topic, msg);
		}

		public void Publish(string topic, byte[] msgBytes)
		{
			_pubClient.PublishMsg(topic, msgBytes);
		}

		public static void PublishMsg(string topic, string msg)
		{
			GetInstance().Publish(topic, msg);
		}

		public static void PublishMsg(string topic, byte[] msgBytes)
		{
			GetInstance().Publish(topic, msgBytes);
		}
	}
}
