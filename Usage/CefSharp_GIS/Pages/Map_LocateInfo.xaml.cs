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
            //this.address.Address = "www.sina.com";
            CefSharp.CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            
            this.address.RegisterJsObject("JsObj", m_TransmitData);             // 向前端页面注册一个JsObj，前端可以通过这个进行交互;
            this.address.BeginInit();                                           // 刷新页面,以便让数据传送到前端;
            this.address.FrameLoadEnd += Address_FrameLoadEnd;                  // 当页面加载完成;
            this.CurrentMapLevel.DataContext = m_TransmitData;
        }

        private void Address_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Address_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if(e.Frame.IsMain)
            {
                // ____________________________________________________获取地图等级;
                this.address.ExecuteScriptAsync(@"
                    document.body.onmouseup = function() {
                        JsObj.getMaplevel(MapLevel);
                    }");
            }
            
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

        /// <summary>
        /// 刷新网页，便于调试;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReloadPage(object sender, RoutedEventArgs e)
        {
            this.address.Reload();
        }

        /// <summary>
        /// 更新地图等级;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeMapLevel(object sender, RoutedEventArgs e)
        {
            WebGISDataModel data = new WebGISDataModel();
            int maplevel = 10;
            try
            {
                maplevel = int.Parse(this.SetMapLevel.Text.ToString());
            }
            catch
            {
                Console.WriteLine("数值输入错误！");
                return;
            }

            data.MapLevel = maplevel;
            data.Longtitude = "116";
            data.Latitude = "39";
            m_TransmitData.m_TransmitData = MapLocationViewModel.ObjectToJson(data);
            this.address.Reload();
            
        }
    }


}
