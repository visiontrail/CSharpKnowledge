using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Xml;
using WPF.Model;

namespace WPF.Control
{
    /// <summary>
    /// 从XML文件中读取出树形结构;
    /// </summary>
    public class XMLTreeViewControl
    {
        private XmlDataProvider xmldata;
        private XmlDocument xmlsource;                             // 方式一：使用Document进行XML文件的读取;
        public List<TreeViewComposite> items;                      // 保存一个完整的树形结构;
        TreeViewComposite root = new TreeViewComposite();          // 组合模式根节点;
        public ObservableCollection<ListViewSearchResult> ret = new ObservableCollection<ListViewSearchResult>();              // 保存检索结果;

        /// <summary>
        /// 在构造器中直接读取一个XML
        /// </summary>
        public XMLTreeViewControl()
        {
            string path = System.IO.Directory.GetCurrentDirectory();        // 获取当前程序的工作路径;
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

        /// <summary>
        /// 递归读取XML文档;
        /// </summary>
        /// <param name="eles"></param>
        /// <param name="father_ele"></param>
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

        public ObservableCollection<ListViewSearchResult> finditems(List<TreeViewComposite> findlist, string search_content)
        {
            foreach(TreeViewComposite iter in findlist)
            {
                if(iter.m_ItemName.Contains(search_content))
                {
                    ListViewSearchResult temp = new ListViewSearchResult();
                    temp.m_ShowContent = iter.m_ItemName;
                    ret.Add(temp);
                }
                if(iter.m_SubList.Count == 0)
                {
                    continue;
                }

                finditems(iter.m_SubList, search_content);
            }

            return ret;
        }

        private static bool FindTreeViewContent(TreeViewComposite iter)
        {
            iter.m_SubList.Find(FindTreeViewContent);
            return true;
        }
    }
}
