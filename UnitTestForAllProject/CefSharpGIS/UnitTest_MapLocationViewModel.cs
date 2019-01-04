// 在文件头中引用要被测试的项目;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CefSharp_GIS;

namespace UnitTestForAllProject
{
    [TestClass]
    public class UnitTest_MapLocationViewModel
    {
        MapLocationViewModel mvm = new MapLocationViewModel();

        [TestMethod]
        public void TestMethod1()
        {
            Console.WriteLine("运行单元测试:" + mvm.m_TransmitData);
            string ret = @"{'LocationList':null,'MapLevel':10,'Longtitude':'116.38778686523436','Latitude':'39.923428952672154'}";

            // 判断是否符合预期;
            Assert.AreEqual(ret, mvm.m_TransmitData);
        }
    }
}
