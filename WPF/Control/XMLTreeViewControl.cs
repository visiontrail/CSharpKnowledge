using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Xml;

namespace WPF.Control
{
    public class XMLTreeViewControl
    {
        private XmlDataProvider xmldata;
        private XmlDocument xmlsource;

        // 实验程序，现在构造器中直接读取一个XML;
        public XMLTreeViewControl()
        {
            xmldata = new XmlDataProvider();
            xmlsource = new XmlDocument();
            xmlsource.Load(@"E:/Workspace/Learn/CSharpLearn/Data/XML/XmlData1.xml");
            readfile();
        }

        private void readfile()
        {
            XmlNodeList eles = xmlsource.DocumentElement.ChildNodes;
            foreach (XmlElement ele in eles)
            {
                Console.WriteLine(ele);
            }
        }
    }
}
