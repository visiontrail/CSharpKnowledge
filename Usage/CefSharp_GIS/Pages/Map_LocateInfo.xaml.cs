using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CefSharp;
using CefSharp.Wpf;

namespace CefSharp_GIS.Pages
{
    /// <summary>
    /// 一个在WPF程序中调用Chrome内核，显示WebGIS的实验小程序;
    /// </summary>
    public partial class Map_LocateInfo : UserControl
    {
        MapLocationViewModel m_TransmitData = new MapLocationViewModel();

        public Map_LocateInfo()
        {
            InitializeComponent();
            this.address.Address = System.Environment.CurrentDirectory + @"\ViewPage\pages\AboutLeafLet.html";
            CefSharp.CefSharpSettings.LegacyJavascriptBindingEnabled = true;

            m_TransmitData.m_Latitude = "39.923428952672154";
            m_TransmitData.m_Longitude = "116.38778686523436";

            this.address.RegisterJsObject("JsObj", m_TransmitData);        // 向前端页面注册一个JsObj，前端可以通过这个进行交互;
            this.address.BeginInit();                                      // 刷新页面,以便让数据传送到前端;
        }

        /// <summary>
        /// 显示Chrome浏览器内核的调试界面;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.address.ShowDevTools();
        }

        private void ReloadPage(object sender, RoutedEventArgs e)
        {
            this.address.Reload();
        }
    }


}
