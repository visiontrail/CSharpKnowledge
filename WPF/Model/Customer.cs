using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Model
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Uri Email { get; set; }
        public bool IsMember { get; set; }
    }

    public class InitCustomerData
    {
        public static ObservableCollection<Customer> InitData()
        {
            ObservableCollection<Customer> list = new ObservableCollection<Customer>();

            list.Add(new Customer() { FirstName = "Guo", LastName = "Liang", Email = new Uri("http://gl925@139.com"), IsMember = true });
            list.Add(new Customer() { FirstName = "Zhang", LastName = "Peng", Email = new Uri("http://Spawn@126.com"), IsMember = true });
            list.Add(new Customer() { FirstName = "Wang", LastName = "WT", Email = new Uri("http://love@126.com"), IsMember = true });
            list.Add(new Customer() { FirstName = "Lv", LastName = "Yang", Email = new Uri("http://Fat@139.com"), IsMember = false });
            list.Add(new Customer() { FirstName = "Wang", LastName = "CY", Email = new Uri("http://WCY@139.com"), IsMember = false });

            return list;
        }
    }

}
