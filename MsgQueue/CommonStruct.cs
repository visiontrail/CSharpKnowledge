using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsgQueue
{
	public class Target
	{
		public string raddr;		//目标地址
		public string laddr;		//本地地址
		public int rport;			//目标端口
		public int lport;			//本地端口
	}

	public class SessionData
	{
		public byte[] data;
		public Target target;

		public SessionData(int dataLen)
		{
			data = new byte[dataLen];
			target = new Target();
		}
	}
}
