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

namespace WPF.UserControlTemplate
{
    /// <summary>
    /// MessageDataGrid.xaml 的交互逻辑
    /// </summary>
    public partial class MessageDataGrid : UserControl
    {
        private object context;
        public object m_context
        {
            get
            {
                return context;
            }
            set
            {
                context = value;
                this.MsgDataGrid3.DataContext = context;
            }
        }


        public MessageDataGrid()
        {
            InitializeComponent();
            this.MsgDataGrid3.CurrentCellChanged += MsgDataGrid3_CurrentCellChanged;
            this.MsgDataGrid3.AddingNewItem += MsgDataGrid3_AddingNewItem;
            this.MsgDataGrid3.CanUserAddRows = true;
        }

        private void MsgDataGrid3_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            if (this.MsgDataGrid3.Items.CurrentItem != null)
            {
                this.MsgDataGrid3.ScrollIntoView(this.MsgDataGrid3.Items[this.MsgDataGrid3.Items.Count]);
            }
        }

        private void MsgDataGrid3_CurrentCellChanged(object sender, EventArgs e)
        {
            if (this.MsgDataGrid3.Items.CurrentItem != null)
            {
                this.MsgDataGrid3.ScrollIntoView(this.MsgDataGrid3.Items[this.MsgDataGrid3.Items.Count]);
            }
        }

    }
}
