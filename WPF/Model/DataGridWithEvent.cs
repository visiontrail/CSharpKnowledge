using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Model
{
    /// <summary>
    /// 这是一个数据模型，这个数据模型在主程序中会关联到一个DataGrid控件当中;
    /// 与DataGridCustomer功能相同，但解决了没有事件交互的缺点;
    /// 这个表的缺点;
    /// 1、固定的表结构
    /// 2、不能实时更新
    /// </summary>
    public class DataGridWithEvent
    {
        public GridCell column1 { get; set; }       // 向单元格填写自定义个类型;
        public GridCell column2 { get; set; }       // 向单元格填写自定义个类型;
        public GridCell column3 { get; set; }       // 向单元格填写自定义个类型;

        // 当表格控件被编辑时，会调用单元格自身实例对应的函数;
        public void JudegePropertyCall_CellEditing(string colHeader)
        {
            switch(colHeader)
            {
                case "Column1":
                    this.column1.EditingCalback();
                    break;

                case "Column2":
                    this.column2.EditingCalback();
                    break;

                case "Column3":
                    this.column3.EditingCalback();
                    break;

                default:
                    break;
            }
        }

    }

    public class InitDataGridWithEventData
    {
        public static ObservableCollection<DataGridWithEvent> InitData()
        {
            ObservableCollection<DataGridWithEvent> list = new ObservableCollection<DataGridWithEvent>();

            list.Add(new DataGridWithEvent() {
                column1 = new GridCell() { name = "123" },
                column2 = new GridCell() { name = "321" },
                column3 = new GridCell() { name = "333" }
            });
            list.Add(new DataGridWithEvent()
            {
                column1 = new GridCell() { name = "asd" },
                column2 = new GridCell() { name = "gfd" },
                column3 = new GridCell() { name = "zxc" }
            });
            list.Add(new DataGridWithEvent()
            {
                column1 = new GridCell() { name = "hjk" },
                column2 = new GridCell() { name = "sda" },
                column3 = new GridCell() { name = "123" }
            });
            return list;
        }
    }
}
