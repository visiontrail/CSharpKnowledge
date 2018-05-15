using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Xml;
using WPF.Model;

namespace WPF.Control
{
    public class XMLTreeViewControl
    {
        private XmlDataProvider xmldata;
        private XmlDocument xmlsource;                             // 方式一：使用Document进行XML文件的读取;
        public List<TreeViewComposite> items;                      // 保存一个完整的树形结构;
        TreeViewComposite root = new TreeViewComposite();

        // 实验程序，现在构造器中直接读取一个XML;
        public XMLTreeViewControl()
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            xmldata = new XmlDataProvider();
            xmlsource = new XmlDocument();
            items = new List<TreeViewComposite>();
            try
            {
                xmlsource.Load(path + @"/XML/XmlData1.xml");                // 读取XML数据;
                XmlNodeList eles = xmlsource.DocumentElement.ChildNodes;
                readfile(eles, null);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
                xmlsource = null;
            }

            
        }

        private void readfile(XmlNodeList eles, TreeViewComposite father_ele)
        {
            
            if(xmlsource == null)
            {
                return;
            }

            if(father_ele == null)
            {
                root = new TreeViewComposite();
                root.m_ItemName = "root";
                father_ele = root;
                items.Add(father_ele);
            }
            
            foreach (XmlElement ele in eles)
            {
                TreeViewComposite item = new TreeViewComposite();
                item.m_ItemName = ele.GetAttribute("Flag").ToString();
                item.m_ItemContent = "This is Message Content!!!!";
                father_ele.m_SubList.Add(item);
                readfile(ele.ChildNodes, item);
            }
        }
    }
}
