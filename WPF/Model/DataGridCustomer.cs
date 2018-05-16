using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Model
{
    public class DataGridCustomer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Uri Email { get; set; }
        public bool IsMember { get; set; }
        public GridCell cell { get; set; }
    }

    public class InitCustomerData
    {
        public static ObservableCollection<DataGridCustomer> InitData()
        {
            ObservableCollection<DataGridCustomer> list = new ObservableCollection<DataGridCustomer>();

            list.Add(new DataGridCustomer() { FirstName = "Guo", LastName = "Liang", Email = new Uri("http://gl925@139.com"), IsMember = true, cell = new GridCell() { name = "123" } });
            list.Add(new DataGridCustomer() { FirstName = "Zhang", LastName = "Peng", Email = new Uri("http://Spawn@126.com"), IsMember = true, cell = new GridCell() { name = "123" } });
            list.Add(new DataGridCustomer() { FirstName = "Wang", LastName = "WT", Email = new Uri("http://love@126.com"), IsMember = true, cell = new GridCell() { name = "123" } });
            list.Add(new DataGridCustomer() { FirstName = "Lv", LastName = "Yang", Email = new Uri("http://Fat@139.com"), IsMember = false, cell = new GridCell() { name = "123" } });
            list.Add(new DataGridCustomer() { FirstName = "Wang", LastName = "CY", Email = new Uri("http://WCY@139.com"), IsMember = false, cell = new GridCell() { name = "123" } });

            return list;
        }
    }

}
