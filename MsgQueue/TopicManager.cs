using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUility;

namespace MsgQueue
{
	/// <summary>
	/// topic管理类
	/// </summary>
	public class TopicManager
	{
		public static TopicManager GetInstance()
		{
			return Singleton<TopicManager>.GetInstance();
		}

		/// <summary>
		/// 注册topic。
		/// </summary>
		/// <param name="topic">模块订阅的topic</param>
		/// <param name="type">对应的数据类型。后面可以用于反射特性</param>
		public void AddTopic(TopicInfo topicInfo)
		{
			if (_topicsMap.ContainsKey(topicInfo.Name))
			{
				return;
			}

			_topicsMap.Add(topicInfo.Name, topicInfo);
		}

		/// <summary>
		/// 判断topic是否已经被订阅
		/// </summary>
		/// <param name="topic"></param>
		/// <returns></returns>
		public bool HasSubscribed(string topic)
		{
			bool bHasSubscribed = false;

			if (!string.IsNullOrWhiteSpace(topic))
			{
				bHasSubscribed = _topicsMap.ContainsKey(topic);
			}

			return bHasSubscribed;
		}

		/// <summary>
		/// 模糊搜索
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public List<TopicInfo> SearchTopic(string key)
		{
			var topicList = new List<TopicInfo>();

			var coll = _topicsMap.Where(kv => kv.Key.IndexOf(key, StringComparison.Ordinal) >= 0).Select(kv => kv.Value);
			topicList.AddRange(coll);
			return topicList;
		}

		private Dictionary<string, TopicInfo> _topicsMap = new Dictionary<string, TopicInfo>();
	}

	public class TopicInfo
	{
		public string Desc { get; private set; }

		public string Name { get; private set; }

		public Type DataType { get; private set; }

		public TopicInfo(string name, string desc, Type type)
		{
			Name = name;
			Desc = desc;
			DataType = type;
		}

		public override string ToString()
		{
			return String.Format($"topic info:\r\nname:{Name}\r\ndesc:{Desc}\r\n对应数据类型:%s", DataType.ToString());
		}
	}
}
