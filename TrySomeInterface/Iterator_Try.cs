using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrySomeInterface
{
    public class Iterator_Try : IEnumerable
    {
        Try_Equatable<int> value1 = new Try_Equatable<int>();
        Try_Equatable<int> value2 = new Try_Equatable<int>();
        
        /// <summary>
        /// 是IEnumerable接口包含的抽象行为;
        /// </summary>
        /// <returns>IEnumerator是一个可用于循环访问集合的对象</returns>
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
