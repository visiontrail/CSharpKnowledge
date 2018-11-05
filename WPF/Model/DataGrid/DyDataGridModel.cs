using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Model
{
    /// <summary>
    /// 该类型主要用来动态生成类型;
    /// 使用DynamicObject作为基类，使用这个类实例所添加的属性都将保存再Properties当中;
    /// </summary>
    public class DyDataDridModel : DynamicObject
    {
        /// <summary>
        /// 用来保存这个动态类型的所有属性;Key为属性的名字，Value为属性的实例;
        /// </summary>
        public Dictionary<string, object> Properties { get; set; }

        /// <summary>
        /// 用来保存中文列名与属性的对应关系;Key为列名（即与属性名对应），Value为列的Header名(即显示名);
        /// </summary>
        public Dictionary<string, string> ColName_Property { get; set; }

        /// <summary>
        /// 优化结构:使用元组保存所有的动态属性;
        /// Item1:属性名;
        /// Item2:列名;
        /// Item3:属性实例;
        /// </summary>
        public List<Tuple<string, string, object>> PropertyList { get; set; }

        /// <summary>
        /// 构造函数，初始化数据;
        /// </summary>
        public DyDataDridModel()
        {
            Properties = new Dictionary<string, object>();
            ColName_Property = new Dictionary<string, string>();
            PropertyList = new List<Tuple<string, string, object>>();
        }
        
        /// <summary>
        /// 系统调用，在为dynamic类型添加属性的时候会自动调用;
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (!Properties.Keys.Contains(binder.Name))
            {
                Properties.Add(binder.Name, value);
            }
            return true;
        }

        // 为动态类型动态添加方法;
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            // 可以通过调用方法的手段添加属性，AddProperty方法一共有三个参数;
            // 参数1：属性的名称;
            // 参数2：属性的实例值;
            // 参数3：列名称与属性之间建立关系;
            if (binder.Name == "AddProperty" && binder.CallInfo.ArgumentCount == 3)
            {
                string name = args[0] as string;
                if (name == null)
                {
                    //throw new ArgumentException("name");  
                    result = null;
                    return false;
                }
                // 向属性列表添加属性及其值;
                object value = args[1];
                Properties.Add(name, value);
                
                // 添加列名与属性列表的映射关系;
                string column_name = args[2] as string;
                ColName_Property.Add(column_name, name);

                PropertyList.Add(new Tuple<string, string, object>(name, column_name, value));

                result = value;
                return true;
            }

            if(binder.Name == "GetProperty" && binder.CallInfo.ArgumentCount == 1)
            {
                string columnname = args[0] as string;
                if (columnname == null)
                {
                    result = null;
                    return false;
                }

                // 在当前列名于属性列表中查找，看是否有匹配项;
                if (ColName_Property.ContainsKey(columnname))
                {
                    string key = ColName_Property[columnname];
                    if (Properties.ContainsKey(key))
                    {
                        object property = Properties[key];
                        result = property;
                        return true;
                    }
                }
                else
                {
                    Console.WriteLine("Can not find the right property");
                    result = null;
                    return false;
                }
            }

            // 判断单元格是否正在被编辑;
            if(binder.Name == "JudgePropertyName_StartEditing" && binder.CallInfo.ArgumentCount == 1)
            {
                string columnname = args[0] as string;
                if(columnname == null)
                {
                    result = null;
                    return false;
                }
                
                // 在当前列名于属性列表中查找，看是否有匹配项;
                if(ColName_Property.ContainsKey(columnname))
                {
                    string key = ColName_Property[columnname];
                    if(Properties.ContainsKey(key))
                    {
                        object property = Properties[key];
                        (property as AbsDataGridCell).EditingCallback();
                        result = property;
                        return true;
                    }
                }
                else
                {
                    Console.WriteLine("Can not find the right property");
                    result = null;
                    return false;
                }
                
            }

            return base.TryInvokeMember(binder, args, out result);
        }

        /// <summary>
        /// 系统调用，会在dynamic类型访问属性时，调用该函数去寻找这个类型中是否存在对应的属性;
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return Properties.TryGetValue(binder.Name, out result);
        }

        /// <summary>
        /// 当单元格失去焦点之后，统一调用单元格类的对应函数;
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool JudgePropertyName_ChangeSelection(string name)
        {
            bool ret = false;

            // 在当前列名于属性列表中查找，看是否有匹配项;
            if (ColName_Property.ContainsKey(name))
            {
                string key = ColName_Property[name];
                if (Properties.ContainsKey(key))
                {
                    object property = Properties[key];
                    (property as AbsDataGridCell).SelectionCellChanged();
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Can not find the right property");
                return false;
            }

            return ret;
        }

    }
}
