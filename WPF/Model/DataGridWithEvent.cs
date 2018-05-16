using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Model
{
    public class DataGridWithEvent
    {
        public GridCell column1 { get; set; }
        public GridCell column2 { get; set; }
        public GridCell column3 { get; set; }
        
        public void JudegePropertyCall(string colHeader)
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
