using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;

namespace MsgQueue
{
	public delegate void HandlerSubscribeMsg(SubscribeMsg msg);

	public class SubscribeMsg
	{
		public string Topic { get; }

		public byte[] Data { get; }

		public SubscribeMsg(byte[] data, string topic)
		{
			Topic = topic;
			Data = data;
		}
	}

	/// <summary>
	/// 消息订阅者。不提供全局的Helper，每个模块定制自己的client
	/// 因为涉及到消息处理函数。
	/// </summary>
	public class SubscribeClient : IDisposable
	{

		public SubscribeClient(int port, string addr = "127.0.0.1")
		{
			_dictionaryTopicHandlers = new Dictionary<string, HandlerSubscribeMsg>();
			_subSocket = new SubscriberSocket($">tcp://{addr}:{port}");
			_subSocket.Options.ReceiveHighWatermark = 10000;
		}

		/// <summary>
		/// 设置订阅消息topic，还要有回调函数，在此处触发一个事件
		/// </summary>
		/// <param name="topic">订阅的主题</param>
		/// <param name="handler">消息处理函数</param>
		/// <returns>true:增加订阅成功；false:订阅失败</returns>
		public bool AddSubscribeTopic(string topic, HandlerSubscribeMsg handler)
		{
			if (_dictionaryTopicHandlers.ContainsKey(topic)) return false;

			_subSocket.Subscribe(topic);
			_dictionaryTopicHandlers[topic] = handler;

			return true;
		}

		/// <summary>
		/// 取消订阅的topic
		/// </summary>
		/// <param name="topic">待取消的主题</param>
		/// <returns></returns>
		public bool CancelSubscribeTopic(string topic)
		{
			if (!_dictionaryTopicHandlers.ContainsKey(topic)) return false;

			_dictionaryTopicHandlers.Remove(topic);
			_subSocket.Unsubscribe(topic);
			return true;
		}

		/// <summary>
		/// 启动任务开始监听订。 TODO 需要优化
		/// </summary>
		public void Run()
		{
			if (_running) return;

			_stop = false;
			_running = true;
			Task.Factory.StartNew(RecvMessage);
		}

		public void Stop()
		{
			_stop = true;
		}

		/// <summary>
		/// 接收消息函数
		/// msg[0] is topic, msg[1] is message content
		/// </summary>
		private void RecvMessage()
		{
			while (!_stop)
			{
				var topic2 = _subSocket.ReceiveFrameBytes();
				var topic = SendReceiveConstants.DefaultEncoding.GetString(topic2);
				var message2 = _subSocket.ReceiveFrameBytes();

				GetTopicHandler(topic)?.Invoke(new SubscribeMsg(message2, topic));
			}
		}

		private HandlerSubscribeMsg GetTopicHandler(string topic)
		{
			return _dictionaryTopicHandlers.ContainsKey(topic) ? _dictionaryTopicHandlers[topic] : null;
		}

		public void Dispose()
		{
			_subSocket?.Close();
			_dictionaryTopicHandlers.Clear();
		}

		private readonly SubscriberSocket _subSocket;
		private readonly Dictionary<string, HandlerSubscribeMsg> _dictionaryTopicHandlers;
		private bool _running;
		private bool _stop;
	}

}
