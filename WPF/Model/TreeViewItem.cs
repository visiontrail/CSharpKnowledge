using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Model
{
    public class TreeViewComposite
    {
        private List<TreeViewComposite> SubList;
        public List<TreeViewComposite> m_SubList
        {
            get
            {
                return SubList;
            }
            set
            {
                SubList = value;
            }
        }

        private string ItemName;
        public string m_ItemName
        {
            get
            {
                return ItemName;
            }
            set
            {
                ItemName = value;
            }
        }

        public TreeViewComposite()
        {
            m_SubList = new List<TreeViewComposite>();
        }
    }
}
