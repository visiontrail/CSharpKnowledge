using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Model
{
    /// <summary>
    /// 该类型主要用来动态生成表格;
    /// 使用Dynamic作为基类，
    /// </summary>
    public class DyDataDridModel : DynamicObject
    {
        Dictionary<string, object> Properties = new Dictionary<string, object>();

        // 为Model动态添加成员;
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if(!Properties.Keys.Contains(binder.Name))
            {
                Properties.Add(binder.Name, value.ToString());
            }
            return true;
        }
        
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return Properties.TryGetValue(binder.Name, out result);
        }
    }
}
