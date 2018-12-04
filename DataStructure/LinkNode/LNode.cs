using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkNode
{
    /// <summary>
    /// 抽象出链表指向Next的节点;
    /// 这样就可以保证一串链表中的每一个数据都可以是不同的类型了;
    /// 原因：TypeLNode<T>都被看做是LNode;
    /// </summary>
    public class LNode
    {
        protected LNode m_Next;

        public LNode(LNode next_node)
        {
            m_Next = next_node;
        }
    }

    /// <summary>
    /// 一个泛型类型的链表;
    /// 他的好处是：不限制链表中类型的种类，可以在数据中填充任意类型;
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TypeLNode<T> : LNode
    {
        public T m_data;
        public TypeLNode(T data) : this(data, null)
        {
        }

        /// <summary>
        /// 创建一个链表中的一个节点，需要它本身的数据和它的下一条数据;
        /// </summary>
        /// <param name="data"></param>
        /// <param name="next_node"></param>
        public TypeLNode(T data, LNode next_node) : base(next_node)
        {
            m_data = data;
        }
    }
}
