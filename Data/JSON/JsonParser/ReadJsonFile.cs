using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;

namespace JsonParser
{
    /// <summary>
    /// 该类型包括了读取JSON对象的三种方法;
    /// </summary>
    public class ReadJsopFile
    {
        public static JObject ReadJsonFileMethodOne(string FilePath)
        {
            StreamReader sr = File.OpenText(FilePath);
            JObject JObj = (JObject)JToken.ReadFrom(new JsonTextReader(sr));
            sr.Close();
            return JObj;
        }
        public static JObject ReadJsonFileMethodTwo(string sFilePath)
        {
            FileStream fs = new FileStream(sFilePath, FileMode.Open);           //初始化文件流;
            byte[] array = new byte[fs.Length];                                 //初始化字节数组;
            fs.Read(array, 0, array.Length);                                    //读取流中数据到字节数组中;
            fs.Close();                                                         //关闭流;
            string str = Encoding.Default.GetString(array);                     //将字节数组转化为字符串;
            JObject JObj = JObject.Parse(str);
            return JObj;
        }
        public static JObject ReadJsonFileMethodThree(string sFilePath)
        {
            FileStream fs = new FileStream(sFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("gb2312"));
            JObject JObj = JObject.Parse(sr.ReadToEnd().ToString());
            fs.Close();
            return JObj;
        }
    }
}
