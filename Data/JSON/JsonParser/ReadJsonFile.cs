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
        /// <summary>
        /// 一个最简单的使用NewtonJson解析一个JSON文件，并反馈JObject对象;
        /// </summary>
        /// <param name="FilePath">Json文件</param>
        /// <returns>Json文件中的对象;</returns>
        public static JObject ReadJsonFileMethodOne(string FilePath)
        {
            StreamReader sr = File.OpenText(FilePath);
            JObject JObj = (JObject)JToken.ReadFrom(new JsonTextReader(sr));
            sr.Close();
            return JObj;
        }

        /// <summary>
        /// 为什么这个做?
        /// </summary>
        /// <param name="FilePath">Json文件</param>
        /// <returns>Json文件中的对象;</returns>
        public static JObject ReadJsonFileMethodTwo(string FilePath)
        {
            FileStream fs = new FileStream(FilePath, FileMode.Open);                    //初始化文件流;
            byte[] array = new byte[fs.Length];                                         //初始化字节数组;
            fs.Read(array, 0, array.Length);                                            //读取流中数据到字节数组中;
            fs.Close();                                                                 //关闭流;
            string str = Encoding.Default.GetString(array);                             //将字节数组转化为字符串;
            JObject JObj = JObject.Parse(str);
            return JObj;
        }

        /// <summary>
        /// 为什么这么做?
        /// </summary>
        /// <param name="FilePath">Json文件</param>
        /// <returns>Json文件中的对象;</returns>
        public static JObject ReadJsonFileMethodThree(string FilePath)
        {
            FileStream fs = new FileStream(FilePath, FileMode.Open, 
                                FileAccess.Read, FileShare.ReadWrite);
            StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("gb2312"));     // 指定编码格式;
            JObject JObj = JObject.Parse(sr.ReadToEnd().ToString());
            fs.Close();
            return JObj;
        }
    }
}
