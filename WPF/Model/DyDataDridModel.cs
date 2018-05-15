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
    /// 使用DynamicObject作为基类，使用这个类实例所添加的属性都将保存再Properties当中;
    /// </summary>
    public class DyDataDridModel : DynamicObject
    {
        // 用来保存这个动态类型的所有属性;
        // string为属性的名字;
        // object为属性的值（同时也包含了类型）;
        Dictionary<string, object> Properties = new Dictionary<string, object>();

        // 为动态类型动态添加成员;
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if(!Properties.Keys.Contains(binder.Name))
            {
                Properties.Add(binder.Name, value);
            }
            return true;
        }

        // 为动态类型动态添加方法;
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            // 可以通过调用方法的手段添加属性;
            if (binder.Name == "AddProperty" && binder.CallInfo.ArgumentCount == 2)
            {
                string name = args[0] as string;
                if (name == null)
                {
                    //throw new ArgumentException("name");  
                    result = null;
                    return false;
                }
                object value = args[1];
                Properties.Add(name, value);
                result = value;
                return true;

            }
            return base.TryInvokeMember(binder, args, out result);
        }

        // 获取属性;
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return Properties.TryGetValue(binder.Name, out result);
        }
        
    }
}
