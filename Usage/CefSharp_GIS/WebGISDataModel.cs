using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CefSharp_GIS
{
    /// <summary>
    /// 这个数据模型，和前端页面的数据模型保持一致;
    /// </summary>
    public class WebGISDataModel
    {
        public string LocationList { get; set; }
        public int MapLevel { get; set; }
        
        public string Longtitude { get; set; }
        public string Latitude { get; set; }
    }

    /// <summary>
    /// WPF端的内部数据模型;
    /// </summary>
    public class MapLocationViewModel : INotifyPropertyChanged
    {
        private string TransmitData = FromDataToJson();             // 方向：WPF --> Chrome 通过该成员传递数据;
        public string m_Longitude { get; set; }
        public string m_Latitude { get; set; }
        private int MapLevel;
        public int m_MapLevel                                       // 方向： Chrome --> WPF 地图等级;
        {
            get { return MapLevel; }
            set
            {
                MapLevel = value;

                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("m_MapLevel"));
                }
            }
        }

        public void getMaplevel(int mapLevel)                        // 获取地图等级;
        {
            m_MapLevel = mapLevel;
            Console.WriteLine("当前地图等级;"+ mapLevel);
        }
        

        /// <summary>
        /// 初始化数据，并将数据转化为JSON格式;
        /// </summary>
        /// <returns></returns>
        private static string FromDataToJson()
        {
            List<string> LocationList = new List<string>();
            WebGISDataModel data = new WebGISDataModel();

            // 初始化地图等级;
            data.MapLevel = 10;

            // 初始化坐标，北京;
            data.Latitude = "39.923428952672154";
            data.Longtitude = "116.38778686523436";

            return ObjectToJson(data);
        }

        public static string ObjectToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public string m_TransmitData
        {
            get
            {
                return TransmitData;
            }
            set
            {
                TransmitData = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}
