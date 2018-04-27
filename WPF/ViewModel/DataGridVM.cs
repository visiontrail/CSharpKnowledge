using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Controls;
using LightMVVM;

namespace WPF.ViewModel
{
    /// <summary>
    /// DataGrid只支持显示如下类型：
    /// 字符串类型、Bool类型、Int\Double等数字类型;
    /// </summary>
    public class DataGridVM : MvvmBase
    {
        // 注册修改列的事件函数;
        public event EventHandler ColumnListChanged;
        // 自定义的列名;
        private ColumnListName ColumnNameList;
        public ColumnListName m_ColumnNameList
        {   
            get { return ColumnNameList; }
            set
            {
                ColumnNameList = value;
                RaisePropertyChanged("ColumnNameList");
                ColumnListChanged(this, null);
                Console.WriteLine("111111");
            }
        }

        //DataGrid的内容容器;
        private List<object> ColumnContent;
        public List<object> m_ColumnContent
        {
            get { return ColumnContent; }
            private set
            {
                ColumnContent = value;
                RaisePropertyChanged("ColumnContent");
                if(ColumnContent.Count > 1000)
                {
                    // 删除最开始的500条记录
                }
            }
        }

        private List<DataGridColumn> ColumnList;
        public List<DataGridColumn> m_ColumList
        {
            get;
            set;
        }

        public DataGridVM(List<string> column_list)
        {
            this.ColumnNameList = new ColumnListName();
            this.ColumnNameList.list = column_list;
        }
    }

    public class ColumnListName
    {
        public event EventHandler ListChanged;
        public List<string> list
        {
            get;set;
        }
        public void Add(string item)
        {
            list.Add(item);
            ListChanged(this, null);
        }
    }

}
